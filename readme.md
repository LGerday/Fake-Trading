# Trading Simulator
Ce programme a pour but de simuler un exchange d'actif (cryptomonnaie) sans mettre en péril son propre argent.
Ce programme utilise l'API de CoinGecko pour les valeurs en temps réel.
## Requirement 
Use [CoinGeckoAsyncApi](https://github.com/tosunthex/CoinGecko) available on [NuGet](https://www.nuget.org/packages/CoinGeckoAsyncApi/).
```bash
pm> Install-Package CoinGeckoAsyncApi -Version 1.6.3
```
And [Ookii.Dialogs.Wpf](https://github.com/ookii-dialogs/ookii-dialogs-wpf) available on [NuGet](https://www.nuget.org/packages/Ookii.Dialogs.Wpf/)
```bash
pm> Install-Package Ookii.Dialogs.Wpf -Version 5.0.1
```
## Screenshot 
### Wallet
![Wallet](https://user-images.githubusercontent.com/98750315/171399602-2c0564f5-f121-405c-a180-26f26822e785.png)
### Spot Buy
![Spot](https://user-images.githubusercontent.com/98750315/171399636-e46479ed-202f-426d-a098-c06e42edd4ac.png)
### Future Buy
![Future](https://user-images.githubusercontent.com/98750315/171399690-bef2ee50-b7e7-434d-bffc-a4627e6c03c2.png)
### APY calculator
![APY](https://user-images.githubusercontent.com/98750315/171399720-0deacdbe-e6ba-4971-b03f-5c812fc4a823.png)
### Settings
![Settings](https://user-images.githubusercontent.com/98750315/171399758-a4159b2d-b7be-4371-9b0a-a5331443eddc.png)
### Set value of wallet
![Import](https://user-images.githubusercontent.com/98750315/171399783-3b20360c-903a-425c-874d-8522a1a64ac4.png)

## Feature 
Cette application aura trois fonctionnalitées principale :
- Trading de cryptomonnaie en spot
- Trading de cryptomonnaie en future
- APY calculator
- Settings
- Import

## Detailed feature:
### Spot 
Achat de crypto actif. Possibilité entre : 
- Bitcoin
- Ethereum
- BNB
- Solana
- XRP
- Avax
- Fantom

### Future :
Achat d'actif (Long ou Short) avec possibilité de levier. Même type d'actif que pour les spots.
### APY calculator
Permet de calculer les revenus sur du stacking sur une periode. Avec effet de compound toute les X periodes.
### Settings
- Changer le montant de votre portefeuille
- Changer le dossier de sauvegarde
- Exporter son historique de trade Spot sous format Excel
- Effacer toute les données enregistrer.

### Import
Permet d'importer des actifs existant en spécifiant le nombre de coin et le prix d'entrée.
