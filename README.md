De online toernooiplanner van de knltb biedt niet meer de mogelijkheid om wedstrijdkaarten te downloaden. Zeker nu het systeem nog traag is, hadden wij enorm de behoefte aan de kaartjes. Dus ik heb een programmatje geschreven dat de kaartjes genereert.

Het is eenvoudig te gebruiken:
- download de .exe en .json
- pas de .json aan voor jouw toernooi en sla hem op
- dubbelklik op de .exe, en er verschijnt een excel-bestand met wedstrijdkaartjes.

In appsettings.json kun je de volgende instellingen vinden:
- Tournament: als je op mijnknltb.toernooi.nl naar jouw toernooi browst, is dit de reeks cijfers en letters achter https://mijnknltb.toernooi.nl/tournament/.
- Date: als je voor een specifieke datum wilt afdrukken. De default waarde is morgen, en deze setting heeft een format van "YYYYMMDD" (bijvoorbeeld "20250309" voor 9 maart 2025).
- OutputFilename: hoe je wilt dat het resultaat-bestand genoemd wordt. Default is "matches". Dus dan wordt een bestand "matches.xlsx" gegenereerd.

Let op! Zorg dat je het gegenereerde Excel bestand afsluit voordat je een nieuwe genereert met dezelfde naam. Anders gaat het genereren mis.
