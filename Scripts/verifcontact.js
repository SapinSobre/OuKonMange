jQuery.validator.unobtrusive.adapters.add("verifcontact", ["Parametre1", "Parametre2"], function(options){
    options.rules["verifcontact"] = options.params;
    options.messages["verifcontact"] = options.message;
});
jQuery.validator.addMethod("verifcontact", function(value, element, params){
    var tel = $("#" + params.parametre1).val();
    var mail = $("#" + params.parametre2).val();
    return tel !== null || mail !== null;
});