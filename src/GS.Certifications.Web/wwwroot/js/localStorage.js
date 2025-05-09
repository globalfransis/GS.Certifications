function saveDataInLocalStorage(key,data){

    window.localStorage.removeItem(key);
    window.localStorage.setItem(key, JSON.stringify(data));
}

function getDataFromLocalStorage(key){

    if(!key)
        return null;

    let data = window.localStorage.getItem(key)
   
    if (!data)
        return null;

    if (data){
        return JSON.parse(data);
    }
}

function clearLocalStorage(){
    window.localStorage.clear()
}