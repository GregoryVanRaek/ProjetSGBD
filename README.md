# Système de Location de Voitures

Ce projet est un système de gestion de location de voitures développé dans le cadre d'un projet SGBD durant mon Bachelier en informatique.
Il comprend plusieurs composants qui mettent en œuvre une architecture trois tiers.
Le projet a été réalise en plusieurs phases : 
1. Analyse du projet
2. Application console
3. Réalisation et intégration de procédures stockées
4. Modification de la couche de présentation avec WPF

## Architecture du Projet

Le projet suit une architecture trois tiers :

1. **Présentation (UI Layer)**
   - Application Console (2_1 - Application console)
   - Application WPF (2_3 - Application WPF)
   - Gère l'interface utilisateur et les interactions

2. **Logique Métier (Business Layer)**
   - Classes métier
   - Validation des données
   - Règles métier (ex: âge minimum de 21 ans pour la location)

3. **Accès aux Données (Data Access Layer)**
   - Procédures stockées (2_2 - procédures)
   - Requêtes SQL
   - Gestion de la persistance des données

## Structure du Projet

```
ProjetSGBD/
├── 1 - Diagramme E-R/          # Documentation du modèle de données
├── 2_1 - Application console/  # Version console de l'application
├── 2_2 - procédures/          # Procédures stockées PostgreSQL
└── 2_3 - Application WPF/     # Interface graphique WPF
```

## Fonctionnalités

### Gestion des Voitures
- Liste des voitures, modèles et catégories
- Ajout de catégories, modèles et voitures
- Suppression de voitures
- Gestion des emplacements (A-F, 1-20)

### Gestion des Clients
- Liste des clients
- Ajout de clients
- Modification des coordonnées
- Vérification des critères (permis de conduire, âge ≥ 21 ans)

### Gestion des Locations
- Liste des voitures actuellement louées
- Liste des voitures disponibles
- Processus de location
- Processus de restitution

## Prérequis

- PostgreSQL
- .NET Framework
- Visual Studio (pour le développement)

## Installation

1. Cloner le repository
2. Créer la base de données PostgreSQL en utilisant les scripts SQL fournis
3. Configurer la chaîne de connexion dans l'application
4. Compiler et exécuter l'application

## Technologies Utilisées

- C# (ADO.net)
- WPF (Windows Presentation Foundation)
- PostgreSQL
- SQL

## Contribution

Ce projet est un travail académique en solo.
