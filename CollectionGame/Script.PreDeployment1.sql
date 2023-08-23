-- Insertion de catégories
INSERT INTO Categories (Nom, Description) VALUES ('Action', 'Jeux d\'action');
INSERT INTO Categories (Nom, Description) VALUES ('Aventure', 'Jeux d\'aventure');
INSERT INTO Categories (Nom, Description) VALUES ('Stratégie', 'Jeux de stratégie');

-- Insertion de jeux vidéo avec catégories
-- Jeux de la catégorie "Action"
INSERT INTO JeuxVideo (Nom, Plateforme, DateDeSortie, CategorieId) VALUES ('Assassin\'s Creed', 'PC', '2007-11-13', 1);
INSERT INTO JeuxVideo (Nom, Plateforme, DateDeSortie, CategorieId) VALUES ('Call of Duty', 'Xbox', '2003-10-29', 1);

-- Jeux de la catégorie "Aventure"
INSERT INTO JeuxVideo (Nom, Plateforme, DateDeSortie, CategorieId) VALUES ('The Legend of Zelda', 'Nintendo Switch', '1986-02-21', 2);
INSERT INTO JeuxVideo (Nom, Plateforme, DateDeSortie, CategorieId) VALUES ('Uncharted', 'PlayStation', '2007-11-19', 2);

-- Jeux de la catégorie "Stratégie"
INSERT INTO JeuxVideo (Nom, Plateforme, DateDeSortie, CategorieId) VALUES ('Civilization VI', 'PC', '2016-10-21', 3);
INSERT INTO JeuxVideo (Nom, Plateforme, DateDeSortie, CategorieId) VALUES ('StarCraft II', 'PC', '2010-07-27', 3);
