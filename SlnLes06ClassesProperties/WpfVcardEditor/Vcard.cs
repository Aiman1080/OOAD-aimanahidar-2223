using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfVcardEditor
{
    internal class Vcard
    {
        public string Achternaam { get; set; }
        public string Voornaam { get; set; }
        public DateTime DatePicker { get; set; }
        public char Gender { get; set; }
        public string PriveEmail {get; set; }
        public string PriveTelefoon { get; set; }
        public string Bedrijf { get; set; }
        public string Job { get; set; }
        public string WerkEmail { get; set; }
        public string WerkTelefoon { get; set; }
        public string Linkdln { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }


        public string GenerateVcardCode()
        {
            string content = "BEGIN:VCARD" + Environment.NewLine;
            content += "VERSION:3.0" + Environment.NewLine;

            if (Voornaam != null && Achternaam != null)
            {
                content += $"FN;CHARSET=UTF-8:{Voornaam} {Achternaam}" + Environment.NewLine;
                content += $"N;CHARSET=UTF-8:{Voornaam};{Achternaam};;;" + Environment.NewLine;
            }

            if (Gender != default(char))
            {
                content += $"GENDER:{Gender}" + Environment.NewLine;
            }

            if (DatePicker != null)
            {
                content += $"BDAY:{DatePicker}" + Environment.NewLine;
            }

            if (PriveEmail != null)
            {
                content += $"EMAIL;CHARSET=UTF-8;type=HOME,INTERNET:{PriveEmail}" + Environment.NewLine;
            }

            if (PriveTelefoon != null)
            {
                content += $"TEL;TYPE=WORK,VOICE:{PriveTelefoon}" + Environment.NewLine;
            }

            if (Bedrijf != null)
            {
                content += $"ORG;CHARSET=UTF-8:{Bedrijf}" + Environment.NewLine;
            }

            if (Job != null)
            {
                content += $"TITLE;CHARSET=UTF-8:{Job}" + Environment.NewLine;
            }

            if (WerkEmail != null)
            {
                content += $"EMAIL;CHARSET=UTF-8;type=WORK,INTERNET:{WerkEmail}" + Environment.NewLine;
            }

            if (WerkTelefoon != null)
            {
                content += $"TEL;TYPE=WORK,VOICE:{WerkTelefoon}" + Environment.NewLine;
            }

            if (Linkdln != null)
            {
                content += $"X-SOCIALPROFILE;TYPE=linkedin:{Linkdln}" + Environment.NewLine;
            }

            if (Facebook != null)
            {
                content += $"X-SOCIALPROFILE;TYPE=facebook:{Facebook}" + Environment.NewLine;
            }

            if (Instagram != null)
            {
                content += $"X-SOCIALPROFILE;TYPE=instagram:{Instagram}" + Environment.NewLine;
            }

            if (Youtube != null)
            {
                content += $"X-SOCIALPROFILE;TYPE=youtube:{Youtube}" + Environment.NewLine;
            }

            content += "END:VCARD";

            return content;
        }

        // Constructors 

        public Vcard() { }

        public Vcard(string vo, string ac, DateTime da, char ge, string pe, string pt, string be, string jo , string we, string wt, string l, string f, string i, string y) 
        {
            Voornaam = vo;
            Achternaam = ac;
            DatePicker = da;
            Gender = ge;
            PriveEmail = pe;
            PriveTelefoon = pt;
            Bedrijf = be;
            Job = jo;
            WerkEmail = we;
            WerkTelefoon = wt;
            Linkdln = l;
            Facebook = f;
            Instagram = i;
            Youtube = y;
        }
    }
}
