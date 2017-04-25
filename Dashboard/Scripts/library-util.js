var LI_LABEL_OPEN = "<li><label>",
	LI_LABEL_CLOSE = "</label></li>";

var LibraryUtil = {
    
    addCommas: function (nStr) {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;

        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }

        return x1 + x2;
    },
    
    showMessage: function (container, text) {
        container.text(text).fadeIn(200).delay(2000).fadeOut(200);
    },

    showError: function (container, text) {
        container.text(text).fadeIn(200).delay(1000).fadeOut(200);
    },

    controlOption: function (select, disabled) {
        $('#' + select + " option").not(":selected").prop("disabled", disabled);
    }
}