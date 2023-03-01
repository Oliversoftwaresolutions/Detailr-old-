(function () {
    var Map = function (element)
    {
        this.element = element;
        this.searchBtn = document.getElementById('search');
        this.postcode = document.getElementsByClassName('js_postcode')[0];
        this.radius = document.getElementsByClassName('js-radius__select')[0];
        this.resultsErrorToast = document.getElementsByClassName('js-toast__error')[0];
        this.resultsCountToast = document.getElementsByClassName("js_toast__results")[0];
        this.resultsCount = document.getElementsByClassName("js_resultsCount")[0];

        this.mapIconMarkers = document.getElementsByClassName("leaflet-marker-icon");
        this.mapIconMarkerShadows = document.getElementsByClassName("leaflet-marker-shadow");

        this.markers = [];

        initMap(this);

    }

    function initMap(map) {
        var mapElement = L.map(map.element).setView([51.505, -0.09], 13);

        L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(mapElement);

        initMapEvents(map, mapElement);
        invokeButtonLoadingState(map);
    }

    function initMapEvents(map, mapElement) {
        map.postcode.addEventListener("focusout", function (event) {
            $.ajax({
                url: '/Business/GetUserLatLon',
                type: 'GET',
                data: { postcode: map.postcode.value },
                success: function (data) {
                    mapElement.flyTo([data.latitude, data.longitude]);
                }
            })
        });

        map.searchBtn.addEventListener('click', function (event) {
            $.ajax({
                url: '/Business/Search',
                type: 'POST',
                data: { postcode: map.postcode.value, radius: map.radius.value },
                success: function (data) {
                    addMarkers(map, data, mapElement);
                    showSearchResultsCountToast(map, data.length);
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                showErrorToast(map);
            }).always(function (data) {
                removeButtonLoadingState(map);
            })
        });
    } 

    function showSearchResultsCountToast(map, data) {

        var openToastEvent = new CustomEvent('openToast');

        var isOneSearchResult = (data == 1) ? "detailer" : "detailers";
        var isOneMileAway = (map.radius.value == 1) ? "mile" : "miles";

        map.resultsCount.textContent = "There is currently at least "
            + data + " " + isOneSearchResult + " which are within "
            + map.radius.value + " " + isOneMileAway + " from " + map.postcode.value;

        map.resultsCountToast.dispatchEvent(openToastEvent);

        
    }

    function showErrorToast(map) {
        var openErrorToastEvent = new CustomEvent('openToast');
        map.resultsErrorToast.dispatchEvent(openErrorToastEvent);
    }

    function invokeButtonLoadingState(map) {
        map.searchBtn.addEventListener('click', function (event) {
            map.searchBtn.classList.add("btn-states--state-b");
        });
    }

    function removeButtonLoadingState(map) {
        map.searchBtn.classList.remove("btn-states--state-b");
    }

    function addMarkers(map, data, mapElement) {
        // Check there are markers on the map
        // and if there are remove them to prevent duplication.
        if (map.mapIconMarkers.length > 0) { 
            for (var i = 0; i < map.mapIconMarkers.length; i++) {
                map.mapIconMarkers[i].remove();
                map.mapIconMarkerShadows[i].remove();
            }
        }

        for (var i = 0; i < data.length; i++) {
            var marker = new L.marker([data[i].latitude, data[i].longitude]);
            map.markers.push(marker);
            marker.addTo(mapElement);
        } 

        // Zoom out of map to fit all markers
        var markerGroup = new L.featureGroup(map.markers);
        mapElement.fitBounds(markerGroup.getBounds());    
    }

    window.Map = Map;

    var map = document.getElementsByClassName("js_map");
    new Map(map[0]);

}());