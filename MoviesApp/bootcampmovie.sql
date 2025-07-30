CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

select * from "Genre";
create table "Genre" (
"Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
"Name" VARCHAR(100) NOT NULL
);

DELETE FROM "Genre" WHERE "Id" = '64ddad74-b9c3-4c21-8c6a-b319fe084f34';
SELECT * FROM "Movie";

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
DELETE FROM "Movie" WHERE "Id" = '5462a9cc-4678-4852-a2c1-a82f9909a011';


SELECT 
    "Movie"."Id" AS MovieId,
    "Movie"."Name" AS MovieName,
    "Movie"."Duration",
    "Movie"."Rating",
    "Movie"."ReleaseYear",
    "Movie"."Description",
    "Genre"."Id" AS GenreId,
    "Genre"."Name" AS GenreName
FROM 
    "Movie"
INNER JOIN 
    "MovieGenre" ON "Movie"."Id" = "MovieGenre"."MovieId"
INNER JOIN 
    "Genre" ON "MovieGenre"."GenreId" = "Genre"."Id"
ORDER BY 
    "Movie"."Name", "Genre"."Name";

SELECT
"Movie"."Id" as MovieId,
"Genre"."Name" as GenreName
FROM
"Movie"
INNER JOIN
"MovieGenre" ON "Movie"."Id" = "MovieGenre"."MovieId"
INNER JOIN
"Genre" ON "MovieGenre"."GenreId" = "Genre"."Id"
ORDER BY
"Movie"."Name", "Genre"."Name";


SELECT g."Name" FROM "MovieGenre" mg
left join "Genre" g on mg."GenreId" = g."Id"
WHERE mg."MovieId" = @id;

select * from "Genre";
SELECT 
    m."Name",
    m."Duration",
    m."Rating",
    m."ReleaseYear",
    ARRAY_AGG(g."Name") AS genres
FROM "Movie" m
LEFT JOIN "MovieGenre" mg ON m."Id" = mg."MovieId"
LEFT JOIN "Genre" g ON mg."GenreId" = g."Id"
GROUP BY m."Id", m."Name", m."Duration", m."Rating", m."ReleaseYear", m."Description"
ORDER BY m."Name";

select * from "MovieView";

drop view "MovieView";
CREATE VIEW "MovieView" AS
SELECT
	m."Id",
	m."Name",
	m."Duration",
	m."ReleaseYear",
	m."Rating",
	STRING_AGG(g."Name", ', ') AS "Genre"	
FROM "Movie" m
LEFT JOIN "MovieGenre" mg  ON m."Id" = mg."MovieId"
LEFT JOIN "Genre" g ON mg."GenreId" = g."Id"
GROUP BY m."Id", m."Name", m."Duration", m."ReleaseYear", m."Rating";      

select * from "BookDetails";

CREATE VIEW "BookDetails" AS
SELECT
	b."Id" AS "BookId",
	b."Title",
	b."Author",
	p."publisherName" AS "Publisher",
	b."Year",
	STRING_AGG(g."Name", ', ') AS "Genre",
	b."ISBN"	
FROM "Book" b
JOIN "Publisher" p  ON b."publisherId" = p."Id"
LEFT JOIN "BookGenre" bg ON b."Id" = bg."bookId"
LEFT JOIN "Genre" g ON bg."genreId" = g."Id"
GROUP BY b."Id", b."Title", b."Author", p."publisherName", b."Year", b."ISBN";
SELECT * FROM "BookDetails";

drop table "Director";

CREATE TABLE "Director"(
    "Id" uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    "Name" varchar(100)
);


INSERT INTO "Director" ("Id", "Name")
VALUES
    ('29d8b843-fafc-4e5f-8d52-83d66b1a9b9c', 'Frank Darabont'),
    ('93b0b945-fc13-4ec8-a2be-0bc2e0f7035b', 'Francis Ford Coppola'),
    ('a441f54c-9811-4f3e-8b57-d0f209bc9ec2', 'Christopher Nolan'),
    ('49badf12-3ef4-4d55-a67a-50743b95bbf3', 'Sidney Lumet'),
    ('a3781e9d-7774-47f1-89b7-2984ac18bcb9', 'Peter Jackson'),
    ('a12232eb-dbac-457e-b6c4-d6ca3605e471', 'Steven Spielberg'),
    ('a4d04ef6-376e-4ec9-a9a7-f8e0078415a8', 'Quentin Tarantino'),
    ('f8ca3c2a-0d89-4c34-8541-1539314f05a5', 'Sergio Leone');




SELECT COALESCE(Genre, '') AS Genre, MovieId, Title FROM Movies;

drop table "MovieLanguage";
drop table "MovieGenre";
DROP TABLE IF EXISTS "Movie";
DROP TABLE "Movie" CASCADE;



CREATE TABLE "Director"(
    "Id" uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    "Name" varchar(100)
);
INSERT INTO "Director" ("Id", "Name")
VALUES
    ('29d8b843-fafc-4e5f-8d52-83d66b1a9b9c', 'Frank Darabont'),
    ('93b0b945-fc13-4ec8-a2be-0bc2e0f7035b', 'Francis Ford Coppola'),
    ('a441f54c-9811-4f3e-8b57-d0f209bc9ec2', 'Christopher Nolan'),
    ('49badf12-3ef4-4d55-a67a-50743b95bbf3', 'Sidney Lumet'),
    ('a3781e9d-7774-47f1-89b7-2984ac18bcb9', 'Peter Jackson'),
    ('a12232eb-dbac-457e-b6c4-d6ca3605e471', 'Steven Spielberg'),
    ('a4d04ef6-376e-4ec9-a9a7-f8e0078415a8', 'Quentin Tarantino'),
    ('f8ca3c2a-0d89-4c34-8541-1539314f05a5', 'Sergio Leone');
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
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
CREATE TABLE "Language" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Name" VARCHAR(100) NOT NULL
);
INSERT INTO "Language" ("Id", "Name") VALUES
('f6176294-4976-426e-931a-450fbcf2614b', 'English'),
('fab532b7-8e1d-4dcf-9279-1395ad302dba', 'German'),
('4cb2d11b-ab3a-4917-942a-fb5bf687df60', 'Spanish'),
('6e9c30e4-67c3-4e0a-adc9-f5347303f555', 'French'),
('310b55c8-10f5-4f88-aa36-f48faa800e05', 'Mandarin'),
('b7436623-258a-4e9d-b239-e399806261f6', 'Arabic');
CREATE TABLE "Movie" (
	"Id" uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
	"Name" varchar(100) NOT NULL,
	"Duration" int NOT  NULL,
	"Rating" float,
	"ReleaseYear" int NOT NULL,
	"Description" varchar(1000),
	"DirectorId" uuid,
	FOREIGN KEY ("DirectorId") REFERENCES "Director"("Id")
);
INSERT INTO "Movie" VALUES ('fad9443a-5894-4c75-bae6-bfa125bf4aa4', 'The Shawshank Redemption', 142, 9.3, 1994, 'A banker convicted of uxoricide forms a friendship over a quarter century with a hardened convict, while maintaining his innocence and trying to remain hopeful through simple compassion.','29d8b843-fafc-4e5f-8d52-83d66b1a9b9c');
INSERT INTO "Movie" VALUES ('d4ea6249-4a9d-4805-8ae4-9944282dea6c', 'The Godfather', 175, 9.2, 1972, 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.','93b0b945-fc13-4ec8-a2be-0bc2e0f7035b');
INSERT INTO "Movie" VALUES ('b37df006-e4de-4571-9473-04ac197cbf2f', 'The Dark Knight', 152, 9.0, 2008, 'When a menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman, James Gordon and Harvey Dent must work together to put an end to the madness.','a441f54c-9811-4f3e-8b57-d0f209bc9ec2');
INSERT INTO "Movie" VALUES ('1158d2b7-a297-47c9-b665-e990c874d507', 'The Godfather Part II', 202, 9.0, 1974, 'The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.','49badf12-3ef4-4d55-a67a-50743b95bbf3');
INSERT INTO "Movie" VALUES ('abfac30b-c1f6-4dfe-8048-b5cb07aa1767', '12 Angry Men', 96, 9.0, 1957, 'The jury in a New York City murder trial is frustrated by a single member whose skeptical caution forces them to more carefully consider the evidence before jumping to a hasty verdict.','a3781e9d-7774-47f1-89b7-2984ac18bcb9');
INSERT INTO "Movie" VALUES ('2ccaf582-56fe-469a-bdfb-04de0376349f', 'The Lord Of the Rings: The Return of the King', 201, 9.0, 2003, 'Gandalf and Aragorn lead the World of Men against Sauron''s army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.','a12232eb-dbac-457e-b6c4-d6ca3605e471');
INSERT INTO "Movie" VALUES ('b0faa90b-77bc-4382-9e7c-5e4853480241', 'Schindler''s List', 195, 9.0, 1993, 'In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.','a4d04ef6-376e-4ec9-a9a7-f8e0078415a8');
INSERT INTO "Movie" VALUES ('64ddad74-b9c3-4c21-8c6a-b319fe084f34', 'Pulp Fiction', 154, 8.8, 1994, 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.','f8ca3c2a-0d89-4c34-8541-1539314f05a5');
INSERT INTO "Movie" VALUES ('4f99c57f-51fd-450e-ac7a-ecb1e64c47e7', 'The Lord Of the Rings: The Fellowship of the Ring', 178, 8.9, 2001, 'A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.','f8ca3c2a-0d89-4c34-8541-1539314f05a5');
INSERT INTO "Movie" VALUES ('952ecd07-a015-4180-8f95-8eaaca79f7c7', 'The Good, the Bad and the Ugly', 178, 8.8, 1966, 'A bounty-hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.','f8ca3c2a-0d89-4c34-8541-1539314f05a5');
CREATE TABLE "MovieLanguage" (
    "MovieId"    UUID NOT NULL,
    "LanguageId" UUID NOT NULL,
    PRIMARY KEY ("MovieId", "LanguageId"),
    FOREIGN KEY ("MovieId")    REFERENCES "Movie"   ("Id") ON DELETE CASCADE,
    FOREIGN KEY ("LanguageId") REFERENCES "Language"("Id") ON DELETE CASCADE
);
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId") VALUES
('fad9443a-5894-4c75-bae6-bfa125bf4aa4', 'f6176294-4976-426e-931a-450fbcf2614b'),
('d4ea6249-4a9d-4805-8ae4-9944282dea6c', 'fab532b7-8e1d-4dcf-9279-1395ad302dba'),
('b37df006-e4de-4571-9473-04ac197cbf2f', '4cb2d11b-ab3a-4917-942a-fb5bf687df60'),
('1158d2b7-a297-47c9-b665-e990c874d507', '6e9c30e4-67c3-4e0a-adc9-f5347303f555'),
('abfac30b-c1f6-4dfe-8048-b5cb07aa1767', '310b55c8-10f5-4f88-aa36-f48faa800e05');
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
CREATE VIEW "MovieView" AS
SELECT
	m."Id",
	m."Name",
	m."Duration",
	m."ReleaseYear",
	m."Rating",
	STRING_AGG(g."Name", ', ') AS "Genre"	
FROM "Movie" m
LEFT JOIN "MovieGenre" mg  ON m."Id" = mg."MovieId"
LEFT JOIN "Genre" g ON mg."GenreId" = g."Id"
GROUP BY m."Id", m."Name", m."Duration", m."ReleaseYear", m."Rating";

CREATE VIEW "MovieFullView" AS
SELECT
	m."Id",
	m."Name",
	m."Duration",
	m."ReleaseYear",
	m."Rating",
	d."Name" AS "DirectorName",
	STRING_AGG(g."Name",  ', ') AS "Genre",
	STRING_AGG(l."Name", ', ') AS "Language"
FROM "Movie" m
LEFT JOIN "Director" d ON m."DirectorId" = d."Id"
LEFT JOIN "MovieGenre" mg ON m."Id" = mg."MovieId"
LEFT JOIN "Genre" g ON mg."GenreId" = g."Id"
LEFT JOIN "MovieLanguage" ml ON m."Id" = ml."MovieId"
LEFT JOIN "Language" l ON ml."LanguageId" = l."Id"
GROUP BY m."Id", m."Name", m."Duration", m."Rating", m."Description", d."Name";

select * from "Director";
select * from "Genre";
select * from "Language";
select * from "Movie";
select * from "MovieLanguage";
select * from "MovieGenre";
select * from "MovieView";

SELECT column_name 
FROM information_schema.columns 
WHERE table_name = 'Movie';


drop view "MovieFullView";

CREATE VIEW "MovieFullView" AS
SELECT
	m."Id",
	m."Name",
	m."Duration",
	m."ReleaseYear",
	m."Rating",
	m."Description",
	d."Name" AS "DirectorName",
	STRING_AGG(g."Name",  ', ') AS "Genre",
	STRING_AGG(l."Name", ', ') AS "Language"
FROM "Movie" m
LEFT JOIN "Director" d ON m."DirectorId" = d."Id"
LEFT JOIN "MovieGenre" mg ON m."Id" = mg."MovieId"
LEFT JOIN "Genre" g ON mg."GenreId" = g."Id"
LEFT JOIN "MovieLanguage" ml ON m."Id" = ml."MovieId"
LEFT JOIN "Language" l ON ml."LanguageId" = l."Id"
GROUP BY m."Id", m."Name", m."Duration", m."Rating", m."Description", d."Name"; 


SELECT m.*
FROM "Movie" m
JOIN "MovieGenre" mg ON m."Id" = mg."MovieId"
JOIN "Genre" g ON mg."GenreId" = g."Id"
WHERE g."Name" IN ('Science Fiction', 'Fantasy');


drop view "MovieFullView";
CREATE VIEW "MovieFullView" AS
SELECT
	m."Id",
	m."Name",
	m."Duration",
	m."ReleaseYear",
	m."Rating",
	m."Description",
	d."Name" AS "DirectorName",
	STRING_AGG(DISTINCT g."Name",  ', ') AS "Genre",
	STRING_AGG(DISTINCT l."Name", ', ') AS "Language"
FROM "Movie" m
LEFT JOIN "Director" d ON m."DirectorId" = d."Id"
LEFT JOIN "MovieGenre" mg ON m."Id" = mg."MovieId"
LEFT JOIN "Genre" g ON mg."GenreId" = g."Id"
LEFT JOIN "MovieLanguage" ml ON m."Id" = ml."MovieId"
LEFT JOIN "Language" l ON ml."LanguageId" = l."Id"
GROUP BY m."Id", m."Name", m."Duration", m."Rating", m."Description", d."Name";


INSERT INTO "Genre" ("Id", "Name") VALUES
('02b49d22-cbc3-48fe-a265-85e0e2c81a7f', 'Fantasy'),
('3f384fe4-a5f0-45fc-891c-7513e63a4dbc', 'Science Fiction')
ON CONFLICT ("Id") DO NOTHING;



INSERT INTO "Genre" ("Name", "Id") VALUES ('Fantasy', '02b49d22-cbc3-48fe-a265-85e0e2c81a7f');
INSERT INTO "Genre" ("Name", "Id") VALUES ('Science Fiction', '3f384fe4-a5f0-45fc-891c-7513e63a4dbc');

DROP TABLE "Movie" CASCADE;
select * from  "Movie" ;
CREATE TABLE "Movie" (
	"Id" uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
	"Name" varchar(100) NOT NULL,
	"Duration" int NOT  NULL,
	"Rating" float,
	"ReleaseYear" int NOT NULL,
	"Description" varchar(1000),
	"DirectorId" uuid,
	FOREIGN KEY ("DirectorId") REFERENCES "Director"("Id")
);
INSERT INTO "Movie" VALUES ('fad9443a-5894-4c75-bae6-bfa125bf4aa4', 'The Shawshank Redemption', 142, 9.3, 1994, 'A banker convicted of uxoricide forms a friendship over a quarter century with a hardened convict, while maintaining his innocence and trying to remain hopeful through simple compassion.','29d8b843-fafc-4e5f-8d52-83d66b1a9b9c');
INSERT INTO "Movie" VALUES ('d4ea6249-4a9d-4805-8ae4-9944282dea6c', 'The Godfather', 175, 9.2, 1972, 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.','93b0b945-fc13-4ec8-a2be-0bc2e0f7035b');
INSERT INTO "Movie" VALUES ('b37df006-e4de-4571-9473-04ac197cbf2f', 'The Dark Knight', 152, 9.0, 2008, 'When a menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman, James Gordon and Harvey Dent must work together to put an end to the madness.','a441f54c-9811-4f3e-8b57-d0f209bc9ec2');
INSERT INTO "Movie" VALUES ('1158d2b7-a297-47c9-b665-e990c874d507', 'The Godfather Part II', 202, 9.0, 1974, 'The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.','49badf12-3ef4-4d55-a67a-50743b95bbf3');
INSERT INTO "Movie" VALUES ('abfac30b-c1f6-4dfe-8048-b5cb07aa1767', '12 Angry Men', 96, 9.0, 1957, 'The jury in a New York City murder trial is frustrated by a single member whose skeptical caution forces them to more carefully consider the evidence before jumping to a hasty verdict.','a3781e9d-7774-47f1-89b7-2984ac18bcb9');
INSERT INTO "Movie" VALUES ('2ccaf582-56fe-469a-bdfb-04de0376349f', 'The Lord Of the Rings: The Return of the King', 201, 9.0, 2003, 'Gandalf and Aragorn lead the World of Men against Sauron''s army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.','a12232eb-dbac-457e-b6c4-d6ca3605e471');
INSERT INTO "Movie" VALUES ('b0faa90b-77bc-4382-9e7c-5e4853480241', 'Schindler''s List', 195, 9.0, 1993, 'In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.','a4d04ef6-376e-4ec9-a9a7-f8e0078415a8');
INSERT INTO "Movie" VALUES ('64ddad74-b9c3-4c21-8c6a-b319fe084f34', 'Pulp Fiction', 154, 8.8, 1994, 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.','f8ca3c2a-0d89-4c34-8541-1539314f05a5');
INSERT INTO "Movie" VALUES ('4f99c57f-51fd-450e-ac7a-ecb1e64c47e7', 'The Lord Of the Rings: The Fellowship of the Ring', 178, 8.9, 2001, 'A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.','f8ca3c2a-0d89-4c34-8541-1539314f05a5');
INSERT INTO "Movie" VALUES ('952ecd07-a015-4180-8f95-8eaaca79f7c7', 'The Good, the Bad and the Ugly', 178, 8.8, 1966, 'A bounty-hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.','f8ca3c2a-0d89-4c34-8541-1539314f05a5');

select * from "Director" ;
INSERT INTO "Director" ("Id", "Name")
VALUES
    ('29d8b843-fafc-4e5f-8d52-83d66b1a9b9c', 'Frank Darabont'),
    ('93b0b945-fc13-4ec8-a2be-0bc2e0f7035b', 'Francis Ford Coppola'),
    ('a441f54c-9811-4f3e-8b57-d0f209bc9ec2', 'Christopher Nolan'),
    ('49badf12-3ef4-4d55-a67a-50743b95bbf3', 'Sidney Lumet'),
    ('a3781e9d-7774-47f1-89b7-2984ac18bcb9', 'Peter Jackson'),
    ('a12232eb-dbac-457e-b6c4-d6ca3605e471', 'Steven Spielberg'),
    ('a4d04ef6-376e-4ec9-a9a7-f8e0078415a8', 'Quentin Tarantino'),
    ('f8ca3c2a-0d89-4c34-8541-1539314f05a5', 'Sergio Leone');


select * from "MovieFullView";
SELECT table_schema, table_name
FROM information_schema.views
WHERE table_name ILIKE '%moviefullview%';
SELECT * FROM "MovieFullView";

SELECT table_schema, table_name
FROM information_schema.views
WHERE table_name ILIKE '%movie%';

ALTER TABLE "Movie"
ADD COLUMN "DirectorId" UUID REFERENCES "Director"("Id") ON DELETE SET NULL;

UPDATE "Movie" SET "DirectorId" = '29d8b843-fafc-4e5f-8d52-83d66b1a9b9c' WHERE "Id" = 'fad9443a-5894-4c75-bae6-bfa125bf4aa4';
UPDATE "Movie" SET "DirectorId" = '93b0b945-fc13-4ec8-a2be-0bc2e0f7035b' WHERE "Id" = 'd4ea6249-4a9d-4805-8ae4-9944282dea6c';
UPDATE "Movie" SET "DirectorId" = 'a441f54c-9811-4f3e-8b57-d0f209bc9ec2' WHERE "Id" = 'b37df006-e4de-4571-9473-04ac197cbf2f';
UPDATE "Movie" SET "DirectorId" = 'a3781e9d-7774-47f1-89b7-2984ac18bcb9' WHERE "Id" = '1158d2b7-a297-47c9-b665-e990c874d507';
UPDATE "Movie" SET "DirectorId" = 'a12232eb-dbac-457e-b6c4-d6ca3605e471' WHERE "Id" = 'abfac30b-c1f6-4dfe-8048-b5cb07aa1767';
UPDATE "Movie" SET "DirectorId" = 'a4d04ef6-376e-4ec9-a9a7-f8e0078415a8' WHERE "Id" = '2ccaf582-56fe-469a-bdfb-04de0376349f';
UPDATE "Movie" SET "DirectorId" = 'f8ca3c2a-0d89-4c34-8541-1539314f05a5' WHERE "Id" = 'b0faa90b-77bc-4382-9e7c-5e4853480241';

CREATE TABLE "Publisher" (
  "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  "publisherName" VARCHAR(100) NOT NULL
);

CREATE TABLE "Book" (
  "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  "Title" VARCHAR(200) NOT NULL,
  "Author" VARCHAR(100) NOT NULL,
  "Year" INT,
  "ISBN" VARCHAR(20),
  "publisherId" UUID REFERENCES "Publisher"("Id")
);

CREATE TABLE "BookGenre" (
  "bookId" UUID NOT NULL REFERENCES "Book"("Id") ON DELETE CASCADE,
  "genreId" UUID NOT NULL REFERENCES "Genre"("Id") ON DELETE CASCADE,
  PRIMARY KEY ("bookId", "genreId")
);


INSERT INTO "Movie" 
VALUES (
  uuid_generate_v4(), 
  'Pulp Fiction', 
  154, 
  8.8, 
  1994, 
  'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.',
  'f8ca3c2a-0d89-4c34-8541-1539314f05a5' -- ID režisera (Quentin Tarantino)
);

UPDATE "Movie"
SET "DirectorId" = 'f8ca3c2a-0d89-4c34-8541-1539314f05a5'
WHERE "Id" = '64ddad74-b9c3-4c21-8c6a-b319fe084f34';










DELETE FROM "Movie" WHERE "Id" = '64ddad74-b9c3-4c21-8c6a-b319fe084f34';

-- Zatim ponovno dodaj:
INSERT INTO "Movie" VALUES (
  '64ddad74-b9c3-4c21-8c6a-b319fe084f34',
  'Pulp Fiction',
  154,
  8.8,
  1994,
  'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.',
  'f8ca3c2a-0d89-4c34-8541-1539314f05a5'
);


UPDATE "Movie" SET "DirectorId" = '29d8b843-fafc-4e5f-8d52-83d66b1a9b9c' WHERE "Id" = 'fad9443a-5894-4c75-bae6-bfa125bf4aa4'; -- The Shawshank Redemption

UPDATE "Movie" SET "DirectorId" = '93b0b945-fc13-4ec8-a2be-0bc2e0f7035b' WHERE "Id" = 'd4ea6249-4a9d-4805-8ae4-9944282dea6c'; -- The Godfather

UPDATE "Movie" SET "DirectorId" = 'a441f54c-9811-4f3e-8b57-d0f209bc9ec2' WHERE "Id" = 'b37df006-e4de-4571-9473-04ac197cbf2f'; -- The Dark Knight

UPDATE "Movie" SET "DirectorId" = '93b0b945-fc13-4ec8-a2be-0bc2e0f7035b' WHERE "Id" = '1158d2b7-a297-47c9-b665-e990c874d507'; -- The Godfather Part II

UPDATE "Movie" SET "DirectorId" = '49badf12-3ef4-4d55-a67a-50743b95bbf3' WHERE "Id" = 'abfac30b-c1f6-4dfe-8048-b5cb07aa1767'; -- 12 Angry Men

UPDATE "Movie" SET "DirectorId" = 'a3781e9d-7774-47f1-89b7-2984ac18bcb9' WHERE "Id" = '2ccaf582-56fe-469a-bdfb-04de0376349f'; -- LOTR: Return of the King

UPDATE "Movie" SET "DirectorId" = 'a12232eb-dbac-457e-b6c4-d6ca3605e471' WHERE "Id" = 'b0faa90b-77bc-4382-9e7c-5e4853480241'; -- Schindler's List

UPDATE "Movie" SET "DirectorId" = 'a4d04ef6-376e-4ec9-a9a7-f8e0078415a8' WHERE "Id" = '64ddad74-b9c3-4c21-8c6a-b319fe084f34'; -- Pulp Fiction

UPDATE "Movie" SET "DirectorId" = 'a3781e9d-7774-47f1-89b7-2984ac18bcb9' WHERE "Id" = '4f99c57f-51fd-450e-ac7a-ecb1e64c47e7'; -- LOTR: Fellowship of the Ring

UPDATE "Movie" SET "DirectorId" = 'f8ca3c2a-0d89-4c34-8541-1539314f05a5' WHERE "Id" = '952ecd07-a015-4180-8f95-8eaaca79f7c7'; -- The Good, the Bad and the Ugly







SELECT
  m."Id",
  m."Name",
  m."Duration",
  m."ReleaseYear",
  m."Rating",
  m."Description",
  d."Name" AS "DirectorName",
  COALESCE(string_agg(DISTINCT g."Name", ','), '') AS "Genres"
FROM "Movie" m
LEFT JOIN "MovieGenre" mg ON m."Id" = mg."MovieId"
LEFT JOIN "Genre" g ON mg."GenreId" = g."Id"
LEFT JOIN "Director" d ON m."DirectorId" = d."Id"
GROUP BY m."Id", m."Name", m."Duration", m."ReleaseYear", m."Rating", m."Description", d."Name"
ORDER BY m."Name"
LIMIT 10 OFFSET 0;



SELECT mg."MovieId", g."Name"
FROM "MovieGenre" mg
JOIN "Genre" g ON mg."GenreId" = g."Id"
LIMIT 10;

SELECT * FROM "MovieFullView" LIMIT 1;

SELECT definition
FROM pg_views
WHERE viewname = 'MovieFullView';

DROP VIEW IF EXISTS "MovieFullView";

CREATE VIEW "MovieFullView" AS
SELECT 
    m."Id",
    m."Name",
    m."Duration",
    m."ReleaseYear",
    m."Rating",
    m."Description",
    d."Name" AS "DirectorName",
    string_agg(DISTINCT g."Name", ', ') AS "Genres",
    string_agg(DISTINCT l."Name", ', ') AS "Languages"
FROM "Movie" m
LEFT JOIN "Director" d ON m."DirectorId" = d."Id"
LEFT JOIN "MovieGenre" mg ON m."Id" = mg."MovieId"
LEFT JOIN "Genre" g ON mg."GenreId" = g."Id"
LEFT JOIN "MovieLanguage" ml ON m."Id" = ml."MovieId"
LEFT JOIN "Language" l ON ml."LanguageId" = l."Id"
GROUP BY 
    m."Id", m."Name", m."Duration", m."ReleaseYear", m."Rating", 
    m."Description", d."Name";


SELECT * FROM "Language";
COMMIT;
SELECT * 
FROM "MovieLanguage"
WHERE "MovieId" = 'fad9443a-5894-4c75-bae6-bfa125bf4aa4';

INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('fad9443a-5894-4c75-bae6-bfa125bf4aa4', 'f6176294-4976-426e-931a-450fbcf2614b');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('d4ea6249-4a9d-4805-8ae4-9944282dea6c', 'fab532b7-8e1d-4dcf-9279-1395ad302dba');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('b37df006-e4de-4571-9473-04ac197cbf2f', '4cb2d11b-ab3a-4917-942a-fb5bf687df60');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('1158d2b7-a297-47c9-b665-e990c874d507', '6e9c30e4-67c3-4e0a-adc9-f5347303f555');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('abfac30b-c1f6-4dfe-8048-b5cb07aa1767', '310b55c8-10f5-4f88-aa36-f48faa800e05');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('2ccaf582-56fe-469a-bdfb-04de0376349f', 'b7436623-258a-4e9d-b239-e399806261f6');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('b0faa90b-77bc-4382-9e7c-5e4853480241', 'f6176294-4976-426e-931a-450fbcf2614b');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('64ddad74-b9c3-4c21-8c6a-b319fe084f34', 'fab532b7-8e1d-4dcf-9279-1395ad302dba');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('4f99c57f-51fd-450e-ac7a-ecb1e64c47e7', '4cb2d11b-ab3a-4917-942a-fb5bf687df60');
INSERT INTO "MovieLanguage" ("MovieId", "LanguageId")
VALUES ('952ecd07-a015-4180-8f95-8eaaca79f7c7', '6e9c30e4-67c3-4e0a-adc9-f5347303f555');

SELECT * FROM "MovieFullView";


SELECT * FROM "MovieFullView"
WHERE "Id" = '1158d2b7-a297-47c9-b665-e990c874d507';


SELECT * FROM "MovieLanguage";


SELECT * FROM "Movie" WHERE "Id" = 'fad9443a-5894-4c75-bae6-bfa125bf4aa4';
SELECT * FROM "Language" WHERE "Id" = 'f6176294-4976-426e-931a-450fbcf2614b';

SHOW search_path;








SELECT * FROM public."MovieLanguage"
WHERE "MovieId" = 'fad9443a-5894-4c75-bae6-bfa125bf4aa4';
COMMIT;
INSERT INTO public."MovieLanguage" ("MovieId", "LanguageId")
VALUES ('fad9443a-5894-4c75-bae6-bfa125bf4aa4', 'f6176294-4976-426e-931a-450fbcf2614b');