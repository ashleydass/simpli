const ACTION_TYPE_REQUEST_SEO = 'REQUEST_SEO';
const ACTION_TYPE_RECEIVED_SEO = 'RECEIVED_SEO';
const initialState = {
  results: [],
  isProcessing: false
}

export const actionCreators = {
  requestSeo: request => async (dispatch, getState) => {
    dispatch({
      type: ACTION_TYPE_REQUEST_SEO
    })

    const { searchSource, query, searchFor } = request
    const url = `api/search?searchSource=${searchSource}&query=${query}&urlPartMatch=${searchFor}`
    const response = await fetch(url)
    const result = await response.json()

    dispatch({
      type: ACTION_TYPE_RECEIVED_SEO,
      result,
      request
    })
  }
}

export const reducer = (state, action) => {
  state = state || initialState

  if (action.type === ACTION_TYPE_REQUEST_SEO) {
    return {
      ...state,
      isProcessing: true
    }
  }

  if (action.type === ACTION_TYPE_RECEIVED_SEO) {
    action.result = {
      ...action.result,
      request: action.request
    }

    return {
      ...state,
      results: [action.result, ...state.results],
      isProcessing: false
    }
  }

  return state
}