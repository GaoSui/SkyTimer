import React, { Component } from 'react'


class List extends Component {
  constructor(props) {
    super(props)
    let lists = JSON.parse(window.localStorage.getItem('lists'))
    if (!lists) {
      lists = [{ time: [] }]
    }
    this.state = { list: lists[0] }
  }

  add(record)
  {
    
  }

  render() {
    return (
      <div>
        <ul>
          {this.state.list.time.map((item, i) =>
            <li key={i}>
              {item.time}
            </li>)}
        </ul>
      </div>
    )
  }
}

export default List