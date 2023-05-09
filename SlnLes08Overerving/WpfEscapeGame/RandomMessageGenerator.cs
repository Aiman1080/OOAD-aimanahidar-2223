using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEscapeGame
{
    public enum MessageType { NoLock, Keys, Other }
    internal class RandomMessageGenerator
    {
        static Random rnd = new Random();
        const int BERICHTEN = 3;
        static string[] keysEr = { "This key doesn't seem to fit.", "Hmm, this key isn't working. Maybe another slot?", "It's still locked, maybe this key isn't the right one." };
        static string[] noLockEr = { "I can't find the lock, where could it be?", "I'm not sure how to use this key. Is there even a lock?", "I've searched everywhere, but I can't find a lock to use this key on." };
        static string[] otherEr = { "I don't think this is how it's supposed to be used...", "I feel silly now, I should only use this on locks.", "What was I thinking? This key is only meant for locks." };

        public static string GetRandomMessage(MessageType t)
        {
            int i = rnd.Next(0, BERICHTEN);
            switch (t)
            {
                case MessageType.Keys:
                    return keysEr[i];

                case MessageType.NoLock:
                    return noLockEr[i];

                case MessageType.Other:
                    return otherEr[i];

                default:
                    return "het moet werken";
            }
        }
    }
}
