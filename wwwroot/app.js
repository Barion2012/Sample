const app = angular.module('testapp', []);
app.config(['$httpProvider', function ($httpProvider) {
	$httpProvider.defaults.headers.post = { 'Content-Type': 'application/json; charset=UTF-8' }
}]);
app.controller('SearchCtrl', ['$http', '$scope', function ($http, $scope) {

	$scope.isAdding = false;
	$scope.job = {
		description: "",
		cost: "",
	};
 
	$scope.getjobs = () => $http.get('/job/list')
			.success((res) =>$scope.jobdata = res)

	$scope.addjob = () => {
		if ($scope.isAdding) {
			$http({
				url: '/job',
				method: 'POST',
				data: $scope.job
			}).success((res) => {
				$scope.isAdding = false;
				$scope.jobdata = res;
			}).error((res)=>alert("Пожалуйста укажите время в формате ЧЧ:ММ"))
		}
		else {
			$scope.isAdding = true;
		}
	}

}]);

