const ACTION_TYPE_REQUEST_SEO = 'REQUEST_SEO';
const ACTION_TYPE_RECEIVED_SEO = 'RECEIVED_SEO';
const ACTION_TYPE_RECEIVED_SEARCH_SOURCES = 'RECEIVED_SEARCH_SOURCES'
const initialState = {
  searchSources: [],
  results: {},
  isProcessing: false
}

const fetchSearchSources = async () => {
  const url = 'api/search/sources'
  const response = await fetch(url)
  const json = await response.json()

  return json.sources
}

export const actionCreators = {
  requestSearchSources: () => async (dispatch, getState) => {
    const searchSources = await fetchSearchSources()

    dispatch({
      type: ACTION_TYPE_RECEIVED_SEARCH_SOURCES,
      searchSources
    })

    return searchSources
  },
  requestSeo: request => async (dispatch, getState) => {
    dispatch({
      type: ACTION_TYPE_REQUEST_SEO
    })

    let { searchSources } = getState().seo

    if (!searchSources || !searchSources.length) {
      this.requestSearchSources()
    }

    const { query, searchFor } = request
    const promises = []

    searchSources.forEach(searchSource => {
      const url = `api/search?searchSource=${searchSource}&query=${query}&urlPartMatch=${searchFor}`
      promises.push(
        fetch(url)
          .then(r => r.json())
          .then(j => {
            dispatch({
              type: ACTION_TYPE_RECEIVED_SEO,
              searchSource,
              result: {
                ...j,
                ...request
              }
            })
          }))
    })

    await Promise.all(promises)
  }
}

export const reducer = (state, action) => {
  state = state || initialState

  if (action.type === ACTION_TYPE_RECEIVED_SEARCH_SOURCES) {
    return {
      ...state,
      searchSources: action.searchSources
    }
  }

  if (action.type === ACTION_TYPE_REQUEST_SEO) {
    return {
      ...state,
      isProcessing: true
    }
  }

  if (action.type === ACTION_TYPE_RECEIVED_SEO) {
    const searchSourceResults = state.results[action.searchSource]
      ? [action.result, ...state.results[action.searchSource]]
      : [action.result]

    return {
      ...state,
      results: {
        ...state.results,
        [action.searchSource]: searchSourceResults
      },
      isProcessing: false
    }
  }

  return state
}