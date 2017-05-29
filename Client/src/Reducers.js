import Actions from './Actions'
const initialState = {
  selectedList: 0,
  lists: [
    {
      name: 'default',
      type: '333',
      time: [
        {
          time: 5660,
          scramble: 'place holder'
        }
      ]
    }
  ]
}

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case Actions.ADD:
      
      return {...state, lists:state.lists}
    default:
      return state
  }
}