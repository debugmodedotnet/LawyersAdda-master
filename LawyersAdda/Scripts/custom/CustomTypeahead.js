$(document).ready(function () {
    
    var bondObjs = {};
    var bondNames = [];

    //get the data to populate the typeahead (plus an id value)
    var throttledRequest = _.debounce(function (query, process) {
        //get the data to populate the typeahead (plus an id value)
        $.ajax({
            url: '/Lawyers/GetLawyersWithImages'
            , cache: false
            , success: function (data) {
                //reset these containers every time the user searches
                //because we're potentially getting entirely different results from the api
                bondObjs = {};
                bondNames = [];
                //Using underscore.js for a functional approach at looping over the returned data.
                _.each(data, function (item, ix, list) {
                    //for each iteration of this loop the "item" argument contains
                    //1 bond object from the array in our json, such as:
                    // { "id":7, "name":"Pierce Brosnan" }
                    //add the label to the display array
                    bondNames.push(item.Name);
                    //also store a hashmap so that when bootstrap gives us the selected
                    //name we can map that back to an id value
                    bondObjs[item.Name] = item;
                });
                //send the array of results to bootstrap for display
                process(bondNames);
            }
        });
    }, 300);


    $(".typeahead").typeahead({
        source: function (query, process) {
            throttledRequest(query, process);
        }
        , highlighter: function (item) {
            var bond = bondObjs[item];

            return '<div class="bond row">'
                  + '<div class="col-xs-2"><img src="' + bond.ImageUrl + '" class="img-responsive img-typeahead"/></div>'
                  + '<div class="col-xs-10"><strong>' + bond.Name + '</strong>'
                  //+ '<div class="col-xs-10"><span>' + bond.Id + '</span>'
                  + '<span style="text-color:#cccccc">&nbsp; (' + bond.City.CityName + ')</span></div>'
                  + '</div>';
        },
        updater: function (selectedName) {
            window.location.href = "/Profile/MemberProfileByID/" + bondObjs[selectedName].Id;
        }
    });
});
