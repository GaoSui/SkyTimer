import React, { Component } from 'react';
import Scramble from './Scramble';
import List from './List'
import './Timer.css'

// let Status = {
//   Zero: 0,
//    Red: 1,
//   Green: 2,
//   Timing: 3,
//   Result: 4,
//   LeftHand: 5,
//   RightHand: 6,
//   LostConnection: 7,
//   Unset: 8
// };

let Colors = { red: 'Timer-red', green: 'Timer-green', black: 'Timer-black' };

class Timer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            time: "00:00:000",
            color: Colors.black,
            type: '333'
        };
        this.ready = false;
    }

    componentDidMount() {
        document.onkeydown = this.keyDownHandler;
        document.onkeyup = this.keyUpHandler;
    }

    componentWillUnmount() {
    }

    keyDownHandler = (e) => {
        if (e.repeat || e.key !== ' ') return;
        if (this.ready) {
            clearInterval(this.tick);
            this.ready = false;
            this.scrambler.update();
        } else {
            this.setState({ color: Colors.red });
            this.readyTimer = setTimeout(() => {
                this.ready = true;
                this.setState({ color: Colors.green });
            }, 1000);
        }
    }

    keyUpHandler = (e) => {
        if (e.repeat || e.key !== ' ') return;
        if (!this.ready) {
            clearTimeout(this.readyTimer);
            this.setState({ color: Colors.black });
            return;
        }

        this.setState({ color: Colors.black });
        this.start = Date.now();
        this.tick = setInterval(() => {
            let mili = Date.now() - this.start;
            let min = Math.floor(mili / 60000);
            mili %= 60000;

            if (min < 10) {
                min = `0${min}`;
            }
            else {
                min = min.toString();
            }

            let sec = Math.floor(mili / 1000);
            mili %= 1000;
            if (sec < 10) {
                sec = `0${sec}`;
            }
            else {
                sec = sec.toString();
            }

            if (mili < 10) {
                mili = `00${mili}`;
            }
            else if (mili < 100) {
                mili = `0${mili}`;
            }
            else {
                mili = mili.toString();
            }

            this.setState({ time: `${min}:${sec}:${mili}` });
        }, 1);
    }

    render() {
        return (
            <div>
                <Scramble type={this.state.type} ref={scramble => this.scrambler = scramble} />
                <p className={this.state.color}>{this.state.time}</p>
                <List ref={list => this.list = list} />
            </div>
        );
    }
}

export default Timer;