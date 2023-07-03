const pswToggle = document.getElementById("show-pswd");
const passwordInput = document.getElementById("password");
let isShowed = false;
pswToggle.addEventListener("change", function(event){
    if(isShowed === false){
        isShowed = true;
        passwordInput.setAttribute("type", "text");
    }
    else {
        isShowed = false;
        passwordInput.setAttribute("type", "password");
    }
});