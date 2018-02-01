var ContactsRow = React.createClass({

    render: function () {
        return (
            <tr>                
                <td>{this.props.item.firstName}</td>
                <td>{this.props.item.lastName}</td>
                <td>{this.props.item.email}</td>
                <td><button class='btn btn-primary'>Edit</button>&nbsp;<button class='btn btn-danger'>Delete</button></td>
            </tr>
        );
    }
});

var ContactsTable = React.createClass({

    getInitialState: function () {

        return {
            result: []
        }
    },
    componentWillMount: function () {

        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var response = JSON.parse(xhr.responseText);

            this.setState({ result: response });

        }.bind(this);
        xhr.send();
    },
    render: function () {
        var rows = [];
        this.state.result.forEach(function (item) {
            rows.push(<ContactsRow key={item.Id} item={item} />);
        });
        return (<table className="table table-hover">
            <thead>
                <tr>                    
                    <th>FirstName</th>
                    <th>LastName</th>
                    <th>Email</th>
                    <td></td>
                </tr>
            </thead>

            <tbody>
                {rows}
            </tbody>

        </table>);
    }

});

ReactDOM.render(<ContactsTable url="http://localhost:50461/api/contacts" />,
    document.getElementById('grid'))