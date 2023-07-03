const historyBox = document.getElementById("historyBox");
const replenishForm = document.getElementById("replenishForm").style;
const withdrawForm = document.getElementById("withdrawForm").style;
const transferForm = document.getElementById("transferForm").style;

const choosenBox = {
    history: false,
    replenish: false,
    withdraw: false,
    transfer: false
}

function ShowHistory(){
    choosenBox.history = PerformChoice(choosenBox.history, historyBox.style);
    historyBox.scrollTop = historyBox.scrollHeight;
}

function Replenish(){
    choosenBox.replenish = PerformChoice(choosenBox.replenish, replenishForm);
}

function Withdraw(){
    choosenBox.withdraw = PerformChoice(choosenBox.withdraw, withdrawForm);
}

function Transfer(){
    choosenBox.transfer = PerformChoice(choosenBox.transfer, transferForm);
}

function PerformChoice(operationType, boxType){
    if(operationType === true){
        boxType.display = "none";
        return false;
    }
    else{
        boxType.display = "block";
        return true;
    }
}