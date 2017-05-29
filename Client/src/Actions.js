
export const Actions = {
  ADD: 'ADD'
}

export const addAction = (time) => {
  return {
    type: Actions.ADD,
    time
  }
}