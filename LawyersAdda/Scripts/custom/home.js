function getC() {
    

        var Countries = ['ARGENTINA',
                    'AUSTRALIA',
                    'BRAZIL',
                    'BELARUS',
                    'BHUTAN',
                    'CHILE',
                    'CAMBODIA',
                    'CANADA',
                    'CHILE',
                    'DENMARK',
                    'DOMINICA'];
        $("#tbservices").autocomplete({
            source: Countries,
            minLength: 0,
            scroll: true,
            autoFocus: true
        }).focus(function () {
            $(this).autocomplete("search", "");
        });
    }