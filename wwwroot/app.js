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

			let validJobData = false;
			let message = "Неизвестная ошибка - смотрите системный лог";
			const timeParts = $scope.job.cost.match(/^(\d{1,2})\:(\d{1,2})$/);

			if ($scope.job.description?.trim().length === 0) {
				message = "Заполните пожалуйста описание задачи";
			}
			else if (timeParts?.length > 0) {
				const h = Number(timeParts[1]), m = Number(timeParts[2]);
				if (h >= 0 && h < 24 && m >= 0 && m < 59 && (h !== 0 || m !== 0)) {
					validJobData = true
				}
				else {
					message = "Пожалуйста укажите время в формате ЧЧ:ММ, потраченное время должно быть больше нуля";
				}
			}
			else {
				message = "Пожалуйста укажите время в формате ЧЧ:ММ, потраченное время должно быть больше нуля";
			}

			if (validJobData) {
				$http({
					url: '/job',
					method: 'POST',
					data: $scope.job
				}).success((res) => {
					$scope.isAdding = false;
					$scope.job.description = "";
					$scope.job.cost = "";
					$scope.jobdata = res;
				}).error((res) => {
					alert(message);
					console.error(res);
				})
			}
			else {
				alert(message);
			}
		}
		else {
			$scope.isAdding = true;
		}
	}

}]);

