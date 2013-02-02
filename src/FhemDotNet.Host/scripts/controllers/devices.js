fhemDotNet.controller('ThermostatListCtrl', function ThermostatListCtrl($scope, $http) {
    $http.get('/devices').success(function (data) {
        $scope.devices = data;
    });
//    $scope.devices = [
//        { name: "Batroom", currentTemp: 20, desiredTemp: 18, mode: 'Manu' }
//    ];
});