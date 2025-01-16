using System;
using System.Collections.Generic;
using System.Linq;

namespace TirociniApp
{
    public class OffertaTirocinio
    {
        public int Id { get; set; }
        public string Azienda { get; set; }
        public string Descrizione { get; set; }
        public bool Approvata { get; set; }
    }

    public class Studente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class Accoppiamento
    {
        public OffertaTirocinio Offerta { get; set; }
        public Studente Studente { get; set; }
        public bool Approvato { get; set; }
    }

    public class ResponsabileTirocini
    {
        public void ValutaOfferta(OffertaTirocinio offerta, bool approvata)
        {
            offerta.Approvata = approvata;
            if (!approvata)
            {
                Console.WriteLine($"Offerta {offerta.Id} rifiutata.");
            }
            else
            {
                Console.WriteLine($"Offerta {offerta.Id} approvata.");
            }
        }

        public void ValutaAccoppiamento(Accoppiamento accoppiamento, bool approvato)
        {
            accoppiamento.Approvato = approvato;
            if (approvato)
            {
                Console.WriteLine("Accoppiamento approvato. Generazione dell'accordo...");
                StampaAccordo(accoppiamento);
            }
            else
            {
                Console.WriteLine("Accoppiamento rifiutato.");
            }
        }

        private void StampaAccordo(Accoppiamento accoppiamento)
        {
            Console.WriteLine("Accordo Tirocinio:");
            Console.WriteLine($"Azienda: {accoppiamento.Offerta.Azienda}");
            Console.WriteLine($"Studente: {accoppiamento.Studente.Nome}");
            Console.WriteLine("Firma richiesta da tutte le parti coinvolte.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var offerte = new List<OffertaTirocinio>();
            var studenti = new List<Studente>();
            var responsabile = new ResponsabileTirocini();

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Crea offerta di tirocinio");
                Console.WriteLine("2. Valuta offerta di tirocinio");
                Console.WriteLine("3. Visualizza offerte approvate");
                Console.WriteLine("4. Aggiungi studente");
                Console.WriteLine("5. Crea accoppiamento");
                Console.WriteLine("6. Valuta accoppiamento");
                Console.WriteLine("7. Esci");

                var scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        Console.Write("Inserisci il nome dell'azienda: ");
                        var azienda = Console.ReadLine();
                        Console.Write("Inserisci la descrizione del tirocinio: ");
                        var descrizione = Console.ReadLine();
                        var nuovaOfferta = new OffertaTirocinio
                        {
                            Id = offerte.Count + 1,
                            Azienda = azienda,
                            Descrizione = descrizione,
                            Approvata = false
                        };
                        offerte.Add(nuovaOfferta);
                        Console.WriteLine("Offerta creata con successo.");
                        break;

                    case "2":
                        Console.Write("Inserisci l'ID dell'offerta da valutare: ");
                        if (int.TryParse(Console.ReadLine(), out int idOfferta))
                        {
                            var offerta = offerte.FirstOrDefault(o => o.Id == idOfferta);
                            if (offerta != null)
                            {
                                Console.Write("Approvare l'offerta? (s/n): ");
                                var approvazione = Console.ReadLine().ToLower() == "s";
                                responsabile.ValutaOfferta(offerta, approvazione);
                            }
                            else
                            {
                                Console.WriteLine("Offerta non trovata.");
                            }
                        }
                        break;

                    case "3":
                        Console.WriteLine("Offerte approvate:");
                        foreach (var offerta in offerte.Where(o => o.Approvata))
                        {
                            Console.WriteLine($"Id: {offerta.Id}, Azienda: {offerta.Azienda}, Descrizione: {offerta.Descrizione}");
                        }
                        break;

                    case "4":
                        Console.Write("Inserisci il nome dello studente: ");
                        var nomeStudente = Console.ReadLine();
                        var nuovoStudente = new Studente
                        {
                            Id = studenti.Count + 1,
                            Nome = nomeStudente
                        };
                        studenti.Add(nuovoStudente);
                        Console.WriteLine("Studente aggiunto con successo.");
                        break;

                    case "5":
                        Console.Write("Inserisci l'ID dell'offerta: ");
                        if (int.TryParse(Console.ReadLine(), out int idOffertaAccoppiamento))
                        {
                            var offerta = offerte.FirstOrDefault(o => o.Id == idOffertaAccoppiamento && o.Approvata);
                            if (offerta != null)
                            {
                                Console.Write("Inserisci l'ID dello studente: ");
                                if (int.TryParse(Console.ReadLine(), out int idStudente))
                                {
                                    var studente = studenti.FirstOrDefault(s => s.Id == idStudente);
                                    if (studente != null)
                                    {
                                        var accoppiamento = new Accoppiamento
                                        {
                                            Offerta = offerta,
                                            Studente = studente
                                        };
                                        Console.WriteLine("Accoppiamento creato. Ora valuta l'accoppiamento.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Studente non trovato.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Offerta non trovata o non approvata.");
                            }
                        }
                        break;

                    case "6":
                        Console.Write("Inserisci l'ID dell'offerta per l'accoppiamento: ");
                        if (int.TryParse(Console.ReadLine(), out int idOffertaValutazione))
                        {
                            var offerta = offerte.FirstOrDefault(o => o.Id == idOffertaValutazione);
                            if (offerta != null && offerta.Approvata)
                            {
                                Console.Write("Inserisci l'ID dello studente: ");
                                if (int.TryParse(Console.ReadLine(), out int idStudenteValutazione))
                                {
                                    var studente = studenti.FirstOrDefault(s => s.Id == idStudenteValutazione);
                                    if (studente != null)
                                    {
                                        var accoppiamento = new Accoppiamento
                                        {
                                            Offerta = offerta,
                                            Studente = studente
                                        };
                                        Console.Write("Approvare l'accoppiamento? (s/n): ");
                                        var approvazione = Console.ReadLine().ToLower() == "s";
                                        responsabile.ValutaAccoppiamento(accoppiamento, approvazione);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Studente non trovato.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Offerta non trovata o non approvata.");
                            }
                        }
                        break;

                    case "7":
                        return;

                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
        }
    }
}
