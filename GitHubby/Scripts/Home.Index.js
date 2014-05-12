function HomeViewModel() {
    var self = this;

    this.searchText = ko.observable("");
    
    this.searchResults = ko.observableArray([]);
    this.hasResults = ko.computed(function() {
        return this.searchResults().length > 0;
    }, this);
    
    this.selectedUser = ko.observable("");
    this.selectedUserFullData = ko.observable();

    this.selectUser = function (user) {
        self.selectedUser(user.login());
        $.getJSON("/api/GitHubApi/GetUser", { username: user.login() }, function success(data) {
            self.selectedUserFullData(new GetUserModel(data.User, data.Repos));
        });
    };
}

function SearchUserModel(user) {
    this.login = ko.observable(user.Login);
    this.avatarUrl = ko.observable(user.AvatarUrl);
}

function GetUserModel(user, repos) {
    this.user = user;
    this.fullHeader = this.user.Login + (this.user.Name !== null ? ' (' + this.user.Name + ')' : "");
    this.publicRepos = repos;
}

var homeViewModel = new HomeViewModel();

homeViewModel.searchText.subscribe(function (value) {
    $.getJSON("/api/GitHubApi/SearchUsers", { search: value }, function success(data) {
        var users = $.map(data, function (user, idx) {
            return new SearchUserModel(user);
        });
        homeViewModel.searchResults(users);
    });
});

ko.applyBindings(homeViewModel);