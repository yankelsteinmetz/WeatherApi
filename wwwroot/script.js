async function requestZmanim(){
    let location = document.getElementById('search').value;

    if (location.trim().length !== 0) {

    let baseUrl = window.location.href;
    let url = baseUrl + `${location}/current`;

    var response = await fetch(url);
    var responseJson = await response.json();
    console.log(responseJson);

    let zmanim = responseJson.Results.Zmanim;

    document.getElementById("location").innerHTML = responseJson.Results.Location.FormattedLocation;
    
    for(let prop of Object.keys(zmanim)){
      
      document.getElementById(prop).innerHTML = zmanim[prop];
    }

    document.getElementById('search').value = null;
  } 
}
document.getElementById('search').addEventListener('change',() => requestZmanim())