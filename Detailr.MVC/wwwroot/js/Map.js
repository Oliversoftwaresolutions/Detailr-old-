var map = L.map('map').setView([51.505, -0.09], 13);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

initMap();

function initMap() {
    searchBusinessesUsingPostcode();
    showPostcodeOnMap(map);
    invokeButtonLoadingState(true);
};

function addMarkerToMap(marker, map) {
    marker.addTo(map);
}

function showPostcodeOnMap(map) {
    $("#postcode").focusout(function () {
        $.ajax({
            url: "/Business/GetUserLtLon",
            type: "GET",
            data: { postcode: $("#postcode").val() },
            success: function (data) {
                map.flyTo([data.latitude, data.longitude]);
            }
        })
    });
}

function searchBusinessesUsingPostcode() {
    $('#search').click(function () {
        $.ajax({
            url: "/Business/Search",
            type: "POST",
            data: { postcode: $("#postcode").val(), radius: $("#radius").find(":selected").val() },
            success: function (data) {

                // Add loading state to search button
                $(".btn-states").addClass("btn-states--state-b");

                addMarkers(data, map);

                showSearchResultCountToast(data.length, postcode.value, radius.value);

            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            showErrorToast();
        }).always(function (dataOrjqXHR, textStatus, jqXHRorErrorThrown) {
            // Remove loading state to search button
            $(".btn-states").removeClass("btn-states--state-b");
        });
    });
};

function showErrorToast() {
    var toast = document.querySelector("#errorToast"),
        openToastEvent = new CustomEvent('openToast');

    toast.dispatchEvent(openToastEvent);
}

function invokeButtonLoadingState(isLoading) {
    var searchButton = document.getElementById('search');

    search.addEventListener('click', function (event) {
        if (isLoading) {
            searchButton.classList.add("btn-states--state-b");
        }
    });
}

function showSearchResultCountToast(data, postcode, radius) {

    var toast = document.querySelector("#resultCountToast"),
        openToastEvent = new CustomEvent('openToast'),
        resultsCount = document.querySelector("#ResultsCount");

    var isOneSearchResult = (data == 1) ? "detailer" : "detailers";
    var isOneMileAway = (radius == 1) ? "mile" : "miles";

    resultsCount.textContent = "There is currently at least " + data + " " +
        isOneSearchResult + " which are within " + radius + " "
        + isOneMileAway + " from " + postcode;


    toast.dispatchEvent(openToastEvent);
}

function addMarkers(data, map) {
    // group all makers together
    var markers = [];

    // Remove all markers first, to prevent duplication
    $(".leaflet-marker-icon").remove();
    $(".leaflet-marker-shadow").remove();

    // Loop through array of businesses and display markers on the map.
    $.each(data, function (index, item) {
        var marker = new L.marker([item.latitude, item.longitude]);
        markers.push(marker);
        addMarkerToMap(marker, map);
    });

    // Zoom out map to fit all markers
    var markerGroup = new L.featureGroup(markers);
    map.fitBounds(markerGroup.getBounds());

}

