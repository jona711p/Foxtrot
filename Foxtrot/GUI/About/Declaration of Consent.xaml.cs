using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Foxtrot.GUI.About
{
    /// <summary>
    /// Interaction logic for Declaration_of_Consent.xaml
    /// </summary>
    public partial class Declaration_of_Consent : Window
    {
        public Declaration_of_Consent()
        {
            InitializeComponent();

            textBox.Text= @"Samtykkerklæring til behandling af personoplysninger
Ved at underskrive denne erklæring giver jeg samtykke til, at de dataansvarlige, der er nævnt nedenfor, behandler følgende personoplysninger om mig.
Mit for- og efternavn
Mit telefon- og fax-nummer 
Min Emailadresse
Navnet på min arbejdsgiver
 
Hvem kan behandle disse oplysninger (dataansvarlige)?
Oplysningerne kan behandles af den kontraktansvarlige partner, andre brugere i systemet, Erhvervsstyrelsen og styrelsen databehandlere.
Hvem kan oplysningerne videregives til (modtagere)?
Andre brugere, Danmarks Statistik, de regionale vækstfora og EU-Kommissionen.
Til hvilket formål kan oplysningerne behandles?
Oplysningerne indsamles og behandles til brug for behandlingen, gennemførelsen og kontrollen af programmet.
 
Hvilke rettigheder giver Persondataloven mig?
Ret til at blive orienteret om indsamling/behandling og videregivelse af oplysninger til brug for elektronisk databehandling (§§ 28 og 29).
Ret til at bede om indsigt i de oplysninger om mig, der behandles elektronisk (§ 31).
Ret til at gøre indsigelse mod, at oplysningerne behandles elektronisk (§ 35).
Ret til at kræve berigtigelse, sletning eller blokering af oplysninger, der e rurigtige, vildlendende eller på lignende måde er behandlet i strid med lovgivningen (§ 37).
 
Accept og signatur:
Jeg bekræfter ved min signatur på denne erklæring, at jeg er:
Indforstået med, at de nævnte dataansvarlige behandler de nævnte personoplysninger om mig, og med at  oplysningerne kan videregives til de nævnte modtagere.
Gjort opmærksom på, at jeg kan klage over behandling af personoplysninger til Datatilsynet, Borgergade 28, 1300 København K, tlf.: 33193200, eller via e-mail: dt@datatilsynet.dk. 
Indforstået med, at det er frivilligt at underskrive denne samtykkeerklæring. Hvis jeg ikke underskriver erklæringen, kan projektet ikke få EU-medfinansiering til min deltagelse/ansættelse i projektet. Jeg kan til enhver tid tilbagekalde mit samtykke.
 
 
 
 
Reference: https://regionalt.erhvervsstyrelsen.dk/sites/default/files/media/samtykkeerklaering_akse_tre_version_2_godkendt_21-11-2016.pdf";
        }
    }
}
