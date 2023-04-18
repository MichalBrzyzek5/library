using Raven.Client.Documents;
using Raven.Client.Documents.BulkInsert;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Session;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var store = new DocumentStore
{
    Urls = new[] { "http://localhost:8080" },
    Database = "Biblioteka"
};
store.Conventions.FindCollectionName = type =>
{
    if (typeof(Ksiazki).IsAssignableFrom(type))
        return "Ksiazka";

        else if (typeof(Uczen).IsAssignableFrom(type))
        return "Uczen";

        else if (typeof(Transakcje).IsAssignableFrom(type))
        return "Transakcje";

    return DocumentConventions.DefaultGetCollectionName(type);
};

store.Initialize();
//Listing();
//Listing2();
//Listing3();
//InsertUczen();
//InsertKsiazki();
//InsertTransakcje();
//Insert2();
Listing4();


//Console.WriteLine("Uczniowie");
void Listing()
{
    using (var session = store.OpenSession())
    {
        var listeUczniow =
            from t in session.Query<Uczen>(collectionName: "Uczen")
            orderby t.Nazwisko
            select t;
        Console.WriteLine(listeUczniow.ToString());

        foreach (var uczen in listeUczniow)
        {
            Console.WriteLine($"{uczen.Id} - {uczen.Imie} - {uczen.Nazwisko}");
        }
    }
}

Console.WriteLine("Ksiazki:");

void Listing2()
{
    using (var session = store.OpenSession())
    {
        var listeKsiazek =
            from t in session.Query<Ksiazki>(collectionName: "Ksiazka")
            orderby t.Tytul
            select t;
        Console.WriteLine(listeKsiazek.ToString());

        foreach (var ksiazka in listeKsiazek)
        {
            Console.WriteLine($"{ksiazka.Id} - {ksiazka.Tytul} - {ksiazka.Autor} - {ksiazka.Status}");
        }
    }
}
void Listing4()
{
    using (var session = store.OpenSession())
    {
        var listeKsiazek =
            from t in session.Query<Ksiazki>(collectionName: "Ksiazka")
         
            orderby t.Status
            
            select t;
        Console.WriteLine(listeKsiazek.ToString());

        foreach (var ksiazka in listeKsiazek)
        {
            Console.WriteLine($"{ksiazka.Id} - {ksiazka.Tytul} - {ksiazka.Autor} - {ksiazka.Status}");
        }
    }
}
void Listing3()
{
    using (var session = store.OpenSession())
    {
        var listeTransakcji =
            from t in session.Query<Transakcje>(collectionName: "Transakcje")
            orderby t.Id_uczen
            select t;
        Console.WriteLine(listeTransakcji.ToString());

        foreach (var Transakcja in listeTransakcji)
        {
            Console.WriteLine($"{Transakcja.Id} - {Transakcja.Id_uczen} - {Transakcja.Id_ksiazka} - {Transakcja.Data_wyp} - {Transakcja.Data_odd}");
        }
    }
}
void Insert2(Raven.Client.Http.RequestExecutor v)
{

    using (var session = store.OpenSession())
    {
        
 session.Store(new Uczen
{
    Imie = "John",
    Nazwisko = "Doe",
    Telefon = "123654380"
});

session.SaveChanges();
    }

}
void InsertUczen(){
    using (BulkInsertOperation bulkInsert = store.BulkInsert())
{
    
        bulkInsert.Store(new Uczen
        {
            Imie = "Michal" ,
            Nazwisko = "Szpak" ,
            Telefon = "144837583"
           
        });
    }
}
void InsertKsiazki(){
    using (BulkInsertOperation bulkInsert = store.BulkInsert())
{
    
        bulkInsert.Store( new Ksiazki
        {
            Tytul = "Lokomotywa" ,
            Autor = "Julian Tuwim" ,
            Status = "Dostepny"
        });
    }
}
void InsertTransakcje(){
    using (BulkInsertOperation bulkInsert = store.BulkInsert())
{
    
        bulkInsert.Store( new Transakcje
        {
            Id_uczen = "uczen/65-A" ,
            Id_ksiazka = "ksiazka/33-A" ,
            Data_wyp = "2023-04-17",
            Data_odd = ""
        });
    }
}



