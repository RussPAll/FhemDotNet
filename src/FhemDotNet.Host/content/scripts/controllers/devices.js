fhemDotNet.controller('ThermostatListCtrl', function ThermostatListCtrl($scope, $http) {
    $http.get('/devices').success(function (data) {
        $scope.devices = data;
    });

    $scope.deviceTemperatureChanged = function (deviceName, newDesiredTemp) {
        $http.put('/devices/' + deviceName + "/desiredTempCommand", { 'NewDesiredTemp': newDesiredTemp}).
            success(function() {
                alert("Saved a device change");
            });
    };
});