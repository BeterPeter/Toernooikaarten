De online toernooiplanner van de knltb biedt niet meer de mogelijkheid om wedstrijdkaarten te downloaden. Zeker nu het systeem nog traag is, hadden wij enorm de behoefte aan de kaartjes. Dus ik heb een programmatje geschreven dat de kaartjes genereert.

Het is eenvoudig te gebruiken:
- download de .exe en .json
- pas de .json aan voor jouw toernooi en sla hem op
- browse op mijnknltb.toernooi.nl naar jouw toernooi & de wedstrijddag waarvoor je kaartjes wil maken
- sla de pagina op in de folder waarin de .exe en .json staan onder de naam schedule.html (is aanpasbaar in de .json)
- dubbelklik op de .exe, en er verschijnt een excel-bestand met wedstrijdkaartjes.

In appsettings.json kun je de volgende instellingen vinden:
- Tournament: niet meer relevant, maar komt in de toekomst waarschijnlijk terug.
- Date: niet meer relevant, maar komt in de toekomst waarschijnlijk terug.
- InputHtmlFileName: de naam van de opgeslagen html pagina waaruit de kaartjes gegenereerd worden (default schedule.html).
- OutputFilename: hoe je wilt dat het resultaat-bestand genoemd wordt. Default is "matches". Dus dan wordt een bestand "matches.xlsx" gegenereerd.

Let op! Zorg dat je het gegenereerde Excel bestand afsluit voordat je een nieuwe genereert met dezelfde naam. Anders gaat het genereren mis.
