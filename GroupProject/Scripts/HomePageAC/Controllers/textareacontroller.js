let TextAreaHomeController = function () {
    let textArea;
    let originalHeight;
    let init = function (container) {
        textArea = $(container);
        
        setOnFocus();
        setOnBlur();
        onKeyDown();
        return ({
            ClearTextArea: ClearTextArea,
            GetTextArea: TextArea
        })
    }
    
    let TextArea = () => textArea;

    let setOnFocus = function () {
        // console.log("inside set on focus controller..>");
        //console.log(textArea);
        $(textArea).focus(function (event) {

            if ($(event.target).val() == "What do you want to say?")
                $(event.target).val("");
        });
    }

    let setOnBlur = function () {
        textArea.blur(function (e) {
            if ($(e.target).val().trim() == "") {
                $(e.target).val("What do you want to say?");
            }
        });
    }
    let onKeyDown = function () {
        originalHeight = textArea.css("height");
        textArea.keydown(() => {
            textArea.css("height", ""); //reset the height
            textArea.css("height", textArea.prop('scrollHeight') + "px");
        })

    }
    let ResetHeight = function () {
        $(textArea).css("height", originalHeight);
    }

    let ClearTextArea = function () {
        console.log("invoked clear text area");
        $(textArea).val("What do you want to say?");
        ResetHeight();
    }

    return { init: init }
}();
