jQuery.validator.addMethod("verifListe", function (value, element, params) {
    var nombreCoche = $('input:checked[data-val-verifListe]').length;
    if (nombreCoche == 0) {
        $('span[data-valmsg-for=ListeDesResto]').text(params.message).removeClass("field-validation-valid").addClass("field-validation-error");
    }
    else {
        $('span[data-valmsg-for=ListeDesResto]').text('');
    }
    return nombreCoche != 0;
});

jQuery.validator.unobtrusive.adapters.add
    ("verifListe", function (options) {
        options.params.message = options.message;
        options.rules["verifListe"] = options.params;
        options.messages["verifListe"] = options.message;
    });