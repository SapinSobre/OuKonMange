var timer;
function ChargeVuePartielle() {
    $.ajax({
        url: '/Vote/AfficheTableau/1',
        type: 'GET', 
        dataType: 'html',
        success: function (result) {
            $('#tableauResultat').html(result);       
        }
    });
}

$(function () {   
    timer = setInterval("ChargeVuePartielle()", 10000);  
    $(function(){ ChargeVuePartielle(); });
});

