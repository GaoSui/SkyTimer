import React, { Component } from 'react';

class Scramble extends Component {
    constructor(props) {
        super(props);
        this.state = { scramble: '' };
    }

    componentDidMount() {
        this.update();
    }

    update() {
        let xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = () => {
            console.log(xhttp.response);
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                this.setState({ scramble: xhttp.responseText });
            }
            else {
                this.setState({ scramble: 'Failed to request scramble' });
            }
        }
        xhttp.open('GET', `http://${window.location.hostname}:5000/scramble/${this.props.type}`, true);
        xhttp.send();
    }

    render() {
        return <p>{this.state.scramble}</p>;
    }
}

export default Scramble;