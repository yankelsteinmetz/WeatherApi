async function requestZmanim(){
  
    let location = document.getElementById('search').value;

    if (location.trim().length !== 0) {

      var response = await fetch(`../${location}/current`);
      var responseJson = await response.json();
      console.log(responseJson);

      let zmanim = responseJson.zmanim;

      document.getElementById("location").innerHTML = responseJson.location.formattedLocation;
      
      for(let prop of Object.keys(zmanim)){
        
        document.getElementById(prop).innerHTML = zmanim[prop];
      }

      document.getElementById('search').value = null;
    } 
}

document.getElementById('search').addEventListener('change',() => requestZmanim())