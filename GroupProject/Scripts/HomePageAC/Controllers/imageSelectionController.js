class ImageSelectionController {
    // inputElement is the <input> element
    // imageElement is the <img/> element of which
    //the image form input element needs to be displayed on
    constructor(inputElement, imageElement) {
        this.inputElement = inputElement;
        this.imageElement = imageElement;
        this.setOnInputChange();
        //this.ChangeImage = this.ChangeImage.bind(this);
        this.DeferedEvent = $.Deferred();
       
    }

    GetImageUploaded() {
        return this.imageElement;
    }

    setOnInputChange() {
        $(this.inputElement).change(()=>this.ChangeImage());
    }

    ChangeImage() {
        let file = $(this.inputElement)[0].files[0];
        //use a reader to convert the file to a URL containing the data (base64string)
        let reader = new FileReader();
        let _this = this;
        reader.onloadend = (event) => {
            let image = new Image().src = reader.result;
            $(_this.imageElement).attr("src", reader.result);
            _this.DeferedEvent.resolve();
        };
        reader.readAsDataURL(file);
    }

    ClearImageUploaded() {
        $(this.imageElement).removeAttr("src");
    }
}

class ProfileImageSelectionController extends ImageSelectionController {
    networkService;
    constructor(inputElement, imageElement) {
        super(inputElement, imageElement);
        this.networkService = NetworkService;
        this.ChangeImage = this.ChangeImage.bind(this);
    }

    ChangeImage() {
        super.ChangeImage();
        this.DeferedEvent.done(
            () => {
                let imageBase64 = $(this.imageElement).attr("src");
                let done = () => console.log("Successfully saved pic on the server");
                let fail = () => alert("Failed to save pic on the server.");
                this.networkService.savePic(imageBase64, done, fail);
            }
        );
    }
}
