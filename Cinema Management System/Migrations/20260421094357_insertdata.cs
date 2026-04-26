using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class insertdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {   // ================= Categories =================
            migrationBuilder.Sql(@"
        INSERT INTO Categories (HomeName, Description) VALUES
        ('Action','Action Movies'),
        ('Comedy','Comedy Movies'),
        ('Drama','Drama Movies'),
        ('Sci-Fi','Science Fiction'),
        ('Horror','Horror Movies'),
        ('Romance','Romantic Movies'),
        ('Adventure','Adventure Movies'),
        ('Fantasy','Fantasy Movies'),
        ('Thriller','Thriller Movies'),
        ('Animation','Animated Movies'),
        ('Crime','Crime Movies'),
        ('Mystery','Mystery Movies'),
        ('Biography','Biographical Movies'),
        ('History','Historical Movies'),
        ('War','War Movies'),
        ('Family','Family Movies'),
        ('Music','Music Movies'),
        ('Sport','Sport Movies'),
        ('Western','Western Movies'),
        ('Documentary','Documentary Movies')
    ");

            // ================= Cinemas =================
            migrationBuilder.Sql(@"
        INSERT INTO Cinemas (CinemaName, Location, Logo) VALUES
        ('VOX Cinema','Cairo','1.png'),
        ('Galaxy Cinema','Giza','2.png'),
        ('IMAX','Alexandria','3.png'),
        ('Cinema City','Nasr City','4.png'),
        ('Renaissance','New Cairo','5.png'),
        ('Stars Cinema','Heliopolis','6.png'),
        ('Downtown Cinema','Cairo','7.png'),
        ('Grand Cinema','Giza','8.png'),
        ('Royal Cinema','Alex','9.png'),
        ('Metro Cinema','Cairo','10.png'),
        ('Sun Cinema','Giza','11.png'),
        ('Moon Cinema','Alex','12.png'),
        ('Sky Cinema','Cairo','13.png'),
        ('Light Cinema','Giza','14.png'),
        ('Magic Cinema','Alex','15.png'),
        ('Prime Cinema','Cairo','16.png'),
        ('Elite Cinema','Giza','17.png'),
        ('Cinema Max','Alex','18.png'),
        ('Cinema Plus','Cairo','19.png'),
        ('Cinema Hub','Giza','20.png')
    ");

            // ================= Movies =================
            migrationBuilder.Sql(@"
        INSERT INTO Movies (Name, MainImg, Price, Description, Status, Date, HomeId, CinemaId) VALUES
        ('Inception','1.jpg',120,'Mind-bending thriller',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Sci-Fi'), (SELECT Id FROM Cinemas WHERE CinemaName='VOX Cinema')),
        ('Titanic','2.jpg',100,'Romantic drama',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Romance'), (SELECT Id FROM Cinemas WHERE CinemaName='Galaxy Cinema')),
        ('The Dark Knight','3.jpg',130,'Batman vs Joker',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Action'), (SELECT Id FROM Cinemas WHERE CinemaName='IMAX')),
        ('Avengers Endgame','4.jpg',150,'Marvel finale',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Action'), (SELECT Id FROM Cinemas WHERE CinemaName='Cinema City')),
        ('Joker','5.jpg',110,'Psychological drama',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Drama'), (SELECT Id FROM Cinemas WHERE CinemaName='Renaissance')),
        ('Interstellar','6.jpg',140,'Space exploration',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Sci-Fi'), (SELECT Id FROM Cinemas WHERE CinemaName='Stars Cinema')),
        ('Frozen','7.jpg',90,'Disney animation',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Animation'), (SELECT Id FROM Cinemas WHERE CinemaName='Downtown Cinema')),
        ('Gladiator','8.jpg',120,'Roman warrior',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='History'), (SELECT Id FROM Cinemas WHERE CinemaName='Grand Cinema')),
        ('The Conjuring','9.jpg',100,'Horror story',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Horror'), (SELECT Id FROM Cinemas WHERE CinemaName='Royal Cinema')),
        ('Fast X','10.jpg',130,'Racing action',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Action'), (SELECT Id FROM Cinemas WHERE CinemaName='Metro Cinema')),
        ('Matrix','11.jpg',125,'Virtual reality',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Sci-Fi'), (SELECT Id FROM Cinemas WHERE CinemaName='Sun Cinema')),
        ('Harry Potter','12.jpg',110,'Magic world',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Fantasy'), (SELECT Id FROM Cinemas WHERE CinemaName='Moon Cinema')),
        ('Shrek','13.jpg',80,'Animated comedy',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Animation'), (SELECT Id FROM Cinemas WHERE CinemaName='Sky Cinema')),
        ('Rocky','14.jpg',95,'Boxing legend',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Sport'), (SELECT Id FROM Cinemas WHERE CinemaName='Light Cinema')),
        ('The Godfather','15.jpg',150,'Mafia story',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Crime'), (SELECT Id FROM Cinemas WHERE CinemaName='Magic Cinema')),
        ('Forrest Gump','16.jpg',100,'Life journey',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Drama'), (SELECT Id FROM Cinemas WHERE CinemaName='Prime Cinema')),
        ('The Lion King','17.jpg',90,'Disney classic',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Animation'), (SELECT Id FROM Cinemas WHERE CinemaName='Elite Cinema')),
        ('Parasite','18.jpg',110,'Korean thriller',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Thriller'), (SELECT Id FROM Cinemas WHERE CinemaName='Cinema Max')),
        ('1917','19.jpg',120,'War movie',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='War'), (SELECT Id FROM Cinemas WHERE CinemaName='Cinema Plus')),
        ('Coco','20.jpg',85,'Music animation',1,GETDATE(), (SELECT Id FROM Categories WHERE HomeName='Music'), (SELECT Id FROM Cinemas WHERE CinemaName='Cinema Hub'))
    ");

            // ================= Actors =================
            migrationBuilder.Sql(@"
        INSERT INTO Actors (Name, Nationality, Image, MovieID) VALUES
        ('Leonardo DiCaprio','USA','1.jpg',(SELECT Id FROM Movies WHERE Name='Inception')),
        ('Kate Winslet','UK','2.jpg',(SELECT Id FROM Movies WHERE Name='Titanic')),
        ('Christian Bale','UK','3.jpg',(SELECT Id FROM Movies WHERE Name='The Dark Knight')),
        ('Robert Downey Jr','USA','4.jpg',(SELECT Id FROM Movies WHERE Name='Avengers Endgame')),
        ('Joaquin Phoenix','USA','5.jpg',(SELECT Id FROM Movies WHERE Name='Joker')),
        ('Matthew McConaughey','USA','6.jpg',(SELECT Id FROM Movies WHERE Name='Interstellar')),
        ('Idina Menzel','USA','7.jpg',(SELECT Id FROM Movies WHERE Name='Frozen')),
        ('Russell Crowe','NZ','8.jpg',(SELECT Id FROM Movies WHERE Name='Gladiator')),
        ('Patrick Wilson','USA','9.jpg',(SELECT Id FROM Movies WHERE Name='The Conjuring')),
        ('Vin Diesel','USA','10.jpg',(SELECT Id FROM Movies WHERE Name='Fast X')),
        ('Keanu Reeves','Canada','11.jpg',(SELECT Id FROM Movies WHERE Name='Matrix')),
        ('Daniel Radcliffe','UK','12.jpg',(SELECT Id FROM Movies WHERE Name='Harry Potter')),
        ('Mike Myers','Canada','13.jpg',(SELECT Id FROM Movies WHERE Name='Shrek')),
        ('Sylvester Stallone','USA','14.jpg',(SELECT Id FROM Movies WHERE Name='Rocky')),
        ('Al Pacino','USA','15.jpg',(SELECT Id FROM Movies WHERE Name='The Godfather')),
        ('Tom Hanks','USA','16.jpg',(SELECT Id FROM Movies WHERE Name='Forrest Gump')),
        ('Donald Glover','USA','17.jpg',(SELECT Id FROM Movies WHERE Name='The Lion King')),
        ('Song Kang-ho','Korea','18.jpg',(SELECT Id FROM Movies WHERE Name='Parasite')),
        ('George MacKay','UK','19.jpg',(SELECT Id FROM Movies WHERE Name='1917')),
        ('Anthony Gonzalez','USA','20.jpg',(SELECT Id FROM Movies WHERE Name='Coco'))
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Categories");
            migrationBuilder.Sql("TRUNCATE TABLE Actors");
            migrationBuilder.Sql("TRUNCATE TABLE Movies");
            migrationBuilder.Sql("TRUNCATE TABLE Cinemas");

        }
    }
}
