using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace verificaXsd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlDocument rilevazioni = new XmlDocument();
            XmlDocument automezzi = new XmlDocument();
            XmlDocument regioni = new XmlDocument();
            XmlDocument proprietari = new XmlDocument();
            XmlDocument cittadini = new XmlDocument();

            //if (File.Exists("./RILEVAZIONI.xml")) { Console.WriteLine("rilevazioni esiste"); }
            //if (File.Exists("./REGIONI.xml")) { Console.WriteLine("regioni esiste"); }
            //if (File.Exists("./CITTADINI.xml")) { Console.WriteLine("cittadini esiste"); }
            //if (File.Exists("./AUTOMEZZI_PROPRIETARI.xml")) { Console.WriteLine("proprietari esiste"); }
            //if (File.Exists("./AUTOMEZZI.xml")) { Console.WriteLine("automezzi esiste"); }

            rilevazioni.Load("./RILEVAZIONI.xml");
            automezzi.Load("./AUTOMEZZI.xml");
            regioni.Load("./REGIONI.xml");
            proprietari.Load("./AUTOMEZZI_PROPRIETARI.xml");
            cittadini.Load("CITTADINI.xml");

            string luogo = "", provincia = "", targa = "", data = "", tempo = "";
            int vel = 0, idRegione = 0;

            foreach (XmlNode rilevazione in rilevazioni.SelectNodes("RILEVAZIONI/RILEVAZIONE"))
            {
                int tempvel = Convert.ToInt16(rilevazione["VEL_RILEVATA"].InnerText), tmpIdRegione = Convert.ToInt16(rilevazione["ID_REGIONE"].InnerText);
                string tmpLuogo = rilevazione["LUOGO"].InnerText, tmpProvincia = rilevazione["PROVINCIA"].InnerText, tmptarga = rilevazione["TARGA"].InnerText, tmpData = rilevazione["DATA"].InnerText, tmpTempo = rilevazione["ORA"].InnerText;

                if (vel < tempvel)
                {
                    vel = tempvel;
                    luogo = tmpLuogo;
                    provincia = tmpProvincia;
                    targa = tmptarga;
                    data = tmpData;
                    tempo = tmpTempo;
                    idRegione = tmpIdRegione;
                }
            }

            string marcaModello = "";

            foreach (XmlNode auto in automezzi.SelectNodes("AUTOVETTURE/AUTOVETTURA"))
            {
                string tmpTarga = auto["TARGA"].InnerText;

                if (tmpTarga == targa)
                {
                    marcaModello = auto["MARCA_MODELLO"].InnerText;
                }
            }

            string FRegione = "";

            foreach (XmlNode regione in regioni.SelectNodes("REGIONI/REGIONE"))
            {
                string tmpRegione = regione["REGIONE"].InnerText;
                int tmpIdRegione = Convert.ToInt16(regione["ID_REGIONE"].InnerText);

                if (idRegione == tmpIdRegione)
                {
                    FRegione = tmpRegione;
                }
            }

            int idCittadino = 0;

            foreach (XmlNode proprietario in proprietari.SelectNodes("AUTOVETTURE_PROPRIETARI/AUTOVETTURA_PROPRIETARIO"))
            {
                string tmpTarga = proprietario["TARGA_AUTOVETTURA"].InnerText;
                int tmpIdCittadino = Convert.ToInt16(proprietario["ID_CITTADINO"].InnerText);

                if (targa == tmpTarga)
                {
                    idCittadino = tmpIdCittadino;
                }
            }

            string cognome = "", nome = "";
            int annoNascita = 0;

            foreach (XmlNode cittadino in cittadini.SelectNodes("CITTADINI/CITTADINO"))
            {
                string tmpCognome = cittadino["COGNOME"].InnerText, tmpNome = cittadino["NOME"].InnerText;
                int tmpIdCittadino = Convert.ToInt16(cittadino["ID_CITTADINO"].InnerText), tmpAnnoNascita = Convert.ToInt16(cittadino["ANNO_NASCITA"].InnerText);

                if (idCittadino == tmpIdCittadino)
                {
                    cognome = tmpCognome;
                    nome = tmpNome;
                    annoNascita = DateTime.Now.Year - tmpAnnoNascita;
                }
            }

                Console.WriteLine("velocita' massima registrata: " + vel);
                Console.WriteLine("targa registrata: " + targa);
                Console.WriteLine("marca e modello del veicolo: " + marcaModello);
                Console.WriteLine("data della registrazione: " + data);
                Console.WriteLine("ora della registrazione: " + tempo);
                Console.WriteLine("luogo della registrazione: " + luogo);
                Console.WriteLine("provincia della registrazione: " + provincia);
                Console.WriteLine("regione della registrazione: " + FRegione);
                Console.WriteLine("nome dell'intestatario: " + nome);
                Console.WriteLine("cognome dell'intestatario: " + cognome);
                Console.WriteLine("eta' dell'intestatario: " + annoNascita);
        }
    }
}

