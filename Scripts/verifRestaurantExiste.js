$('input#Nom').on("keyup", function () {
    $.ajax({
        url: '/Restaurant/VerifRestaurantExiste/',
        data: "Nom=" + $('input#Nom').val(),
        type: 'GET',
        dataType: 'html',
        //loadingElementId: 'chargement',
        success: function (result) {
            $('#verifRestaurantExiste').html(result);
        }
    })
})

