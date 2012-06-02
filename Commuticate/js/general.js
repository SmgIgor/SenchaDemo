; (function () {
    $('html').removeClass('no-js').addClass('js');
})();

; function Init_MaxLength() {
    $('textarea[maxlength], input[type=text]')
		.keyup(function () { EvaluateMax($(this, '.charsRemaining')); })
		.each(function () { EvaluateMax($(this, '.charsRemaining')); });
}

; function EvaluateMax(control, result) {
    var max = parseInt($(control).attr('maxlength'));

    if (max != NaN) {
        if ($(control).val().length > max) {
            $(control).val($(control).val().substr(0, max));
        }

        $(control).nextAll(result).first().html('You have ' + (max - $(control).val().length) + ' characters remaining');
    }
}
