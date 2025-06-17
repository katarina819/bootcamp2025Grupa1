CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

DROP TABLE IF EXISTS "MovieGenre";
DROP TABLE IF EXISTS "Movie";
DROP TABLE IF EXISTS "Genre";

create table "Genre" (
"Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
"Name" VARCHAR(100) NOT NULL
);

INSERT INTO "Genre" ("Name", "Id") VALUES ('Action', '1d81712d-a70b-4695-ac02-491632c216de');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Comedy', '74e903a7-235e-48c1-baec-edfea0f4d933');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Drama','66a7f115-06fd-4267-a609-8be5b2e3cc91');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Horror', '9fcc68cf-4f1c-4b89-b979-b523ca22c9b6');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Science Fiction', '3f384fe4-a5f0-45fc-891c-7513e63a4dbc');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Romance', '99c3a8e3-7b67-429f-8618-609356ad7747');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Thriller', 'dd5c8c36-f907-4490-a8b6-28017ce6b001');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Fantasy', '02b49d22-cbc3-48fe-a265-85e0e2c81a7f');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Documentary', '54a4dac6-1019-41eb-bf80-3e02ffd64f7d');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Animation', 'fe0ffb6e-f503-4372-8aed-79a89c8a6d0f');

select * from "Genre";

CREATE TABLE "Movie" (
	"Id" uuid PRIMARY KEY,
	"Name" varchar(100),
	"Duration" int,
	"Rating" float,
	"ReleaseYear" int,
	"Description" varchar(1000)
);

INSERT INTO "Movie" VALUES ('fad9443a-5894-4c75-bae6-bfa125bf4aa4', 'The Shawshank Redemption', 142, 9.3, 1994, 'A banker convicted of uxoricide forms a friendship over a quarter century with a hardened convict, while maintaining his innocence and trying to remain hopeful through simple compassion.');
INSERT INTO "Movie" VALUES ('d4ea6249-4a9d-4805-8ae4-9944282dea6c', 'The Godfather', 175, 9.2, 1972, 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.');
INSERT INTO "Movie" VALUES ('b37df006-e4de-4571-9473-04ac197cbf2f', 'The Dark Knight', 152, 9.0, 2008, 'When a menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman, James Gordon and Harvey Dent must work together to put an end to the madness.');
INSERT INTO "Movie" VALUES ('1158d2b7-a297-47c9-b665-e990c874d507', 'The Godfather Part II', 202, 9.0, 1974, 'The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.');
INSERT INTO "Movie" VALUES ('abfac30b-c1f6-4dfe-8048-b5cb07aa1767', '12 Angry Men', 96, 9.0, 1957, 'The jury in a New York City murder trial is frustrated by a single member whose skeptical caution forces them to more carefully consider the evidence before jumping to a hasty verdict.');
INSERT INTO "Movie" VALUES ('2ccaf582-56fe-469a-bdfb-04de0376349f', 'The Lord Of the Rings: The Return of the King', 201, 9.0, 2003, 'Gandalf and Aragorn lead the World of Men against Sauron''s army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.');
INSERT INTO "Movie" VALUES ('b0faa90b-77bc-4382-9e7c-5e4853480241', 'Schindler''s List', 195, 9.0, 1993, 'In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.');
INSERT INTO "Movie" VALUES ('64ddad74-b9c3-4c21-8c6a-b319fe084f34', 'Pulp Fiction', 154, 8.8, 1994, 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.');
INSERT INTO "Movie" VALUES ('4f99c57f-51fd-450e-ac7a-ecb1e64c47e7', 'The Lord Of the Rings: The Fellowship of the Ring', 178, 8.9, 2001, 'A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.');
INSERT INTO "Movie" VALUES ('952ecd07-a015-4180-8f95-8eaaca79f7c7', 'The Good, the Bad and the Ugly', 178, 8.8, 1966, 'A bounty-hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.');

SELECT * FROM "Movie";

CREATE TABLE "MovieGenre" (
  "MovieId" UUID NOT NULL,
  "GenreId" UUID NOT NULL,
  PRIMARY KEY ("MovieId", "GenreId"),
  FOREIGN KEY ("MovieId") REFERENCES "Movie"("Id") ON DELETE CASCADE,
  FOREIGN KEY ("GenreId") REFERENCES "Genre"("Id") ON DELETE CASCADE
);

INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('fad9443a-5894-4c75-bae6-bfa125bf4aa4', '1d81712d-a70b-4695-ac02-491632c216de');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('d4ea6249-4a9d-4805-8ae4-9944282dea6c', '74e903a7-235e-48c1-baec-edfea0f4d933');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('b37df006-e4de-4571-9473-04ac197cbf2f', '66a7f115-06fd-4267-a609-8be5b2e3cc91');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('1158d2b7-a297-47c9-b665-e990c874d507', '9fcc68cf-4f1c-4b89-b979-b523ca22c9b6');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('abfac30b-c1f6-4dfe-8048-b5cb07aa1767', '3f384fe4-a5f0-45fc-891c-7513e63a4dbc');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('2ccaf582-56fe-469a-bdfb-04de0376349f', '99c3a8e3-7b67-429f-8618-609356ad7747');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('b0faa90b-77bc-4382-9e7c-5e4853480241', 'dd5c8c36-f907-4490-a8b6-28017ce6b001');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('64ddad74-b9c3-4c21-8c6a-b319fe084f34', '02b49d22-cbc3-48fe-a265-85e0e2c81a7f');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('4f99c57f-51fd-450e-ac7a-ecb1e64c47e7', '54a4dac6-1019-41eb-bf80-3e02ffd64f7d');
INSERT INTO "MovieGenre" ("MovieId", "GenreId") VALUES ('952ecd07-a015-4180-8f95-8eaaca79f7c7', 'fe0ffb6e-f503-4372-8aed-79a89c8a6d0f');

select * from "MovieGenre";
