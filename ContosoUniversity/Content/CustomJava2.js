

    $(document).ready(function () {

       
        $("#CountryID").change(function () {
         
            $("#ProvinceID").empty();
            $.get('/Home/GetProvincesForCountry/' + $(this).val(), function (response) {
                var parsedJson = $.parseJSON(response);


                $("#ProvinceID").append("<option value='0'>Select a Province</option>");
                $.each(parsedJson, function (k, v) {
                    // $('#Province').append($('<option>').text(v.text).attr('value', v.val));
                    $("#ProvinceID").append("<option value='" + v.Value + "'>" + v.Text + "</option>");
                });
            });
        });
        
        $("#ProvinceID").change(function () {
            $("#CityID").empty();
            $.get('/Home/GetCitiesForProvince/' + $(this).val(), function (response) {
                var parsedJson = $.parseJSON(response);


                $("#CityID").append("<option value='0'>Select a City</option>");
                $("#CityID").append("<option value='933050'>UNKNOWN</option>");
                $.each(parsedJson, function (k, v) {
                    // $('#Province').append($('<option>').text(v.text).attr('value', v.val));
                    $("#CityID").append("<option value='" + v.Value + "'>" + v.Text + "</option>");
                });
            });
        });

        $("#NumberOfTanks").change(function () {
            var rowCount = $('.phoneRow').length;
            $("#phoneList").append('<div class="phoneRow"><select name="Tanks[' + rowCount + '].ProductClass" class="phoneType">@foreach (SelectListItem item in new ContosoUniversity.Models.Tank().ProductClasses){<option value="@item.Value">@item.Text</option>}</select><input type="text" name="Tanks[' + rowCount + '].Number" class="phoneNumber" /></div>');

            var insertCode = '<br />'
            + '<%= Html.DropDownListFor(model => model.Tanks[' + rowCount + '].ProductClass,  Model.Countries, "Select Product Class") %>'
            + '<%= Html.TextBoxFor(model => model.SafeFill)%>'

            $("#phoneList").append(insertCode);
        });


        $("#EntityID").change(function () {
            $("#DivisionID").empty();
            $.get('/Home/GetDivisionsForEntity/' + $(this).val(), function (response) {
                var parsedJson = $.parseJSON(response);


                $("#DivisionID").append("<option value='0'>Select a Division</option>");
                $.each(parsedJson, function (k, v) {
                    // $('#Province').append($('<option>').text(v.text).attr('value', v.val));
                    $("#DivisionID").append("<option value='" + v.Value + "'>" + v.Text + "</option>");
                });
            });
        });




    });
