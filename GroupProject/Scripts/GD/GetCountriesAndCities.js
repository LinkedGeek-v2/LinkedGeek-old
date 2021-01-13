let GetCountriesAndCities = (countries, cities) => function () {
    let citiesArr = [];
    let countriesID = [];

    $("#country").one("focus", function () {
        $.getJSON("/address/countries", function (data) {
            $.each(data, function (key, entry) {
                countries.append($("<option data-country-iso = '" + entry.countryIsoID + "' ></option > ").attr("value", entry.countryName).text(entry.countryName));
                countriesID.push(entry.countryIsoID);
            });
        });
    });

    //event handler that checks if country has value and enables city choice
    $("#country").on("change", function () {
        let chosenCountryID = $("#countries option[value='" + this.value + "']").attr("data-country-iso");
        if (!countriesID.includes(chosenCountryID)) {
            this.value = "";
            $("#city").attr("disabled", true);
            $("input[name=countryIsoID]").val("");
        }
        else {
            $("input[name=countryIsoID]").val(chosenCountryID);
        }

        citiesArr.length = 0;
        $("#city").val("");
        cities.empty();
        if (countriesID.includes(chosenCountryID)) {
            $("#city").attr("disabled", false);
            $.getJSON("/address/cities/" + chosenCountryID, function (data) {
                $.each(data, function (key, entry) {
                    cities.append($("<option data-cityid='" + entry.cityID + "'></option>").attr("value", entry.cityName).text(entry.cityName));
                    citiesArr.push(entry.cityID);
                });
            });
        }
    });

    $("#city").on("change", function () {
        let chosenCityID = $("#cities option[value='" + this.value + "']").attr("data-cityid");
        chosenCityID = parseInt(chosenCityID);
        if (!citiesArr.includes(chosenCityID)) {
            this.value = "";
            $("input[name=cityID]").val("");
        }
        else {
            $("input[name=cityID]").val(chosenCityID);
        }
    });
}(countries, cities);