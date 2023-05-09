using ConsoleKassaTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfEscapeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Room currentRoom;
        public MainWindow()
        {
            InitializeComponent();



            // Define items living room
            Item key1 = new Item(
                "small silver key",
                "A small silver key, reminds me of one I had in high school.",
                true
            );
            Item key2 = new Item(
                "large key",
                "A large key. Could this be my way out? ",
                true
            );

            Item locker = new Item(
            "locker",
            "A locker. I wonder what's inside. ",
            false
            );
            locker.HiddenItem = key2;
            locker.IsLocked = true;
            locker.Key = key1;

            Item bed = new Item(
            "bed",
            "Just a bed. I am not tired right now. ",
            false
            );

            bed.HiddenItem = key1;

            Item chair = new Item(
                "chair", 
                "This is a chair, but there is nothing.",
                false
            );
            Item poster = new Item(
                "poster",
                "The poster from Ronaldo...",
                true
            );

            // Define item living room
            Item clock = new Item("clock", "A large clock.");
            Item flowerpot = new Item("flowerpot", "I attempted to add some color to the room with this plant, but I'm not sure if I was successful.");
            Item closet = new Item("closet", "A closet with some basic furnitures, I never really use it.");

            // Define item computer room 
            Item painting = new Item("painting", "Is that a replica of a famous painting by Picasso?");
            Item computer = new Item("computer", "The computer I use for both schoolwork and gaming.") ;
            Item radiator = new Item("radiator", "A radiator, which is necessary to keep the room warm.");

            // Define doors
            Door BedroomToLivingroom = new Door("groene deur", true, key2);
            Door LivingroomToBedroom = new Door("groene deur", true);
            Door LivingroomToComputerroom = new Door("linkse deur", false);
            Door ComputerroomToLivingroom = new Door("rechtse deur", false);
            Door LivingroomToNull = new Door("de deur met code", true);

            // Define list
            List<Door> livingRoomDoors = new List<Door> { LivingroomToBedroom, LivingroomToComputerroom };

            // define room
            Room bedroom = new Room(
                "bedroom",
                "I seem to be in a medium sized bedroom. There is a locker to the left, a nice rug on the floor, and a bed to the right.",
                BedroomToLivingroom
            );
            Room livingRoom = new Room(
                "living room",
                "there is sofa on my left, a TV right in front of me and a closet on the right side.",
                livingRoomDoors
            );
            Room computerRoom = new Room(
                "computer room",
                "there is a pc on a desk on my left as well as a famous painting on the wall, a radiator in front of me and a bin right next to it.",
                ComputerroomToLivingroom
            );
            Room nullRoom = new Room(
                "secret room",
                "I dont know anything about this room...",
                LivingroomToNull
            );

            BedroomToLivingroom.AnotherRoom = livingRoom;
            LivingroomToBedroom.AnotherRoom = bedroom;
            LivingroomToComputerroom.AnotherRoom = computerRoom;
            ComputerroomToLivingroom.AnotherRoom = livingRoom;
            LivingroomToNull.AnotherRoom = nullRoom;

            // setup bedroom
            bedroom.Items.Add(new Item(
            "floor mat",
            "A bit ragged floor mat, but still one of the most popular designs. "
            ));

            // add
            bedroom.Items.Add(bed);
            bedroom.Items.Add(locker);
            bedroom.Items.Add(chair);
            bedroom.Items.Add(poster);

            livingRoom.Items.Add(clock);
            livingRoom.Items.Add(flowerpot);
            livingRoom.Items.Add(closet);

            computerRoom.Items.Add(painting);
            computerRoom.Items.Add(computer);
            computerRoom.Items.Add(radiator);

            // start game
            currentRoom = bedroom;
            lblMessage.Content = "I am awake, but cannot remember who I am!? Must have been a hell of a party last night... ";
            txtRoomDesc.Text = currentRoom.Description;
            UpdateUI();
        }

        private void UpdateUI()
        {
            lstRoomItems.Items.Clear();
            foreach (Item i in currentRoom.Items)
            {
                lstRoomItems.Items.Add(i);
            }
            lstRoomDoors.Items.Clear();
            foreach (Door d in currentRoom.EasyEntryDoors)
            {
                lstRoomDoors.Items.Add(d.Name);
            }

            string roomName = currentRoom.Name;
            imgRoom.Source = new BitmapImage(new Uri($"img/{roomName}.png", UriKind.Relative));
        }

        private void lstRoomItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCheck.IsEnabled = lstRoomItems.SelectedValue != null; // room item selected
            btnPickUp.IsEnabled = lstRoomItems.SelectedValue != null; // room item selected
            btnUseOn.IsEnabled = lstRoomItems.SelectedValue != null && lstMyItems.SelectedValue != null; // room item and picked up item selected
            btnOpenWith.IsEnabled = lstRoomDoors.SelectedValue != null && lstMyItems.SelectedValue != null;
            btnEnter.IsEnabled = lstRoomDoors.SelectedValue != null;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            // 1. find item to check
            Item roomItem = (Item)lstRoomItems.SelectedItem;
            // 2. is it locked?
            if (roomItem.IsLocked)
            {
                lblMessage.Content = $"{roomItem.Description}It is firmly locked. ";
                return;
            }

            // 3. does it contain a hidden item?
            Item foundItem = roomItem.HiddenItem;
            if (foundItem != null)
            {
                lblMessage.Content = $"Oh, look, I found a {foundItem.Name}. ";
                lstMyItems.Items.Add(foundItem);
                roomItem.HiddenItem = null;
                return;
            }

            // 4. just another item; show description
            lblMessage.Content = roomItem.Description;
        }

        private void btnUseOn_Click(object sender, RoutedEventArgs e)
        {
            // 1. find both items
            Item myItem = (Item)lstMyItems.SelectedItem;
            Item roomItem = (Item)lstRoomItems.SelectedItem;

            // 2. item doesn't fit
            if (roomItem.Key != myItem)
            {
                switch (roomItem.ToString())
                {
                    case "locker":
                        lblMessage.Content = RandomMessageGenerator.GetRandomMessage(MessageType.Keys);
                        break;

                    case "chair":
                    case "poster":
                    case "bed":
                        lblMessage.Content = RandomMessageGenerator.GetRandomMessage(MessageType.NoLock);
                        break;

                    default:
                        lblMessage.Content = RandomMessageGenerator.GetRandomMessage(MessageType.Other);
                        break;
                }
                return;
            }

            // 3. item fits; other item unlocked
            roomItem.IsLocked = false;
            roomItem.Key = null;
            lstMyItems.Items.Remove(myItem);
            lblMessage.Content = $"I just unlocked the {roomItem.Name}!";
        }

        private void btnPickUp_Click(object sender, RoutedEventArgs e)
        {
            // 1. find selected item
            Item selItem = (Item)lstRoomItems.SelectedItem;

            if (!selItem.IsPortable)
            {
                lblMessage.Content = $"I don't think picking up the {selItem} will help me get out...";
                return;
            }

            // 2. add item to your items list
            lblMessage.Content = $"I just picked up the {selItem.Name}. ";
            lstMyItems.Items.Add(selItem);
            lstRoomItems.Items.Remove(selItem);
            currentRoom.Items.Remove(selItem);
        }

        private void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            Item geselecteerdeItem = (Item)lstMyItems.SelectedItem;
            lstMyItems.Items.Remove(geselecteerdeItem);
            lstRoomItems.Items.Add(geselecteerdeItem);
        }

        private void lstMyItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUseOn.IsEnabled = lstRoomItems.SelectedValue != null && lstMyItems.SelectedValue != null;
            btnCheck.IsEnabled = lstMyItems.SelectedValue != null;
            btnDrop.IsEnabled = lstMyItems.SelectedValue != null;
        }
        private Door CheckDoor()
        {
            string door = lstRoomDoors.SelectedItem.ToString();
            Door d = new Door();

            for (int i = 0; i < currentRoom.EasyEntryDoors.Count; i++)
            {
                if (currentRoom.EasyEntryDoors[i].Name == door)
                {
                    d = currentRoom.EasyEntryDoors[i];
                    return d;
                }
            }
            return d;
        }
        private void btnOpenWith_Click(object sender, RoutedEventArgs e)
        {
            Item i = (Item)lstMyItems.SelectedItem;
            Door d = CheckDoor();

            if (d.Key != i)
            {
                lblMessage.Content = RandomMessageGenerator.GetRandomMessage(MessageType.Keys);
                return;
            }

            d.IsLocked = false;

            lblMessage.Content = "The door is unlocked!";
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Door d = (Door)lstRoomDoors.SelectedItem;
            Door dr = CheckDoor();

            if (!d.IsLocked)
            {
                currentRoom = d.AnotherRoom;
                UpdateUI();
            }
            else
            {
                lblMessage.Content = "the door is locked.";
            }
        }
    }
}
