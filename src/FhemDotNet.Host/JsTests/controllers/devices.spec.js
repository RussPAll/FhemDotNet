describe("FhemDotNet", function () {
    describe("Controllers", function () {
        beforeEach(module('fhemDotNet'));
        describe("ThermostatListCtrl", function () {
            it("should download device data",
                inject(function (_$httpBackend_, $rootScope, $controller) {
                    _$httpBackend_.whenGET('/devices').
                        respond([{ one: 'test' }, { one: 'test'}]);

                    var scope = $rootScope.$new(),
                        ctrl = $controller("ThermostatListCtrl", { $scope: scope });
                    _$httpBackend_.flush();
                    expect(scope.devices.length).toBe(2);
                })
            );
        });
    });
});