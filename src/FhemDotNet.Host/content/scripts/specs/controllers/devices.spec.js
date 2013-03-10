/// <reference path="../../angular/angular.js"/>
/// <reference path="../../angular/angular-mocks.js"/>
/// <reference path="../../app.js"/>
/// <reference path="../../controllers/devices.js"/>

describe("FhemDotNet", function () {
    describe("Controllers", function () {
        beforeEach(module('fhemDotNet'));
        it('should execute', function () {
            expect(true).toBe(true);
        });
        describe("ThermostatListCtrl", function () {
            it('should download device data', function() {
                inject(function(_$httpBackend_, $rootScope, $controller) {
                    _$httpBackend_.whenGET('/devices').
                        respond([{ one: 'test' }, { one: 'test'}]);

                    var scope = $rootScope.$new(),
                        ctrl = $controller("ThermostatListCtrl", { $scope: scope });
                    _$httpBackend_.flush();
                    expect(scope.devices.length).toBe(2);
                })
            });
        });
    });
});