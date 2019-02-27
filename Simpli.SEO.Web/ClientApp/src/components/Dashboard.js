import React, { Component } from 'react'
import { Row, Col } from 'react-bootstrap'
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Seo';

class Dashboard extends Component {
  constructor(props) {
    super(props)

    this.state = {
      searchSource: 'google',
      query: '',
      searchFor: ''
    }

    this.handleSubmit = this.handleSubmit.bind(this)
    this.handleChange = this.handleChange.bind(this)
    this.getResultRows = this.getResultRows.bind(this)
  }

  handleChange(e) {
    this.setState({
      [e.target.id]: e.target.value
    })
  }

  handleSubmit(e) {
    e.preventDefault()

    const { requestSeo } = this.props
    requestSeo(this.state)
  }

  getResultRows() {
    const { results } = this.props
    const total = results.length
    return results.map((m, i) => {
      const { matches, timestamp, request } = m
      const { searchSource, query, searchFor } = request
      const time = new Date(timestamp)
      return (
        <tr key={i}>
          <th scope="row">{total - i}</th>
          <td>{time.getUTCFullYear() +"/"+ (time.getUTCMonth()+1) +"/"+ time.getUTCDate() + " " + time.getUTCHours() + ":" + time.getUTCMinutes() + ":" + time.getUTCSeconds()}</td>
          <td>{searchSource}</td>
          <td>{query}</td>
          <td>{searchFor}</td>
          <td>{matches.join(',')}</td>
        </tr>
      )})
  }

  render() {
    return (
      <div>
        <h1>Dashboard</h1>
        <Row>
          <Col md={5}>
            <form>
              <div className="form-group">
                <label htmlFor="query">Query</label>
                <input type="text" className="form-control" id="query" placeholder="Query"
                       onChange={this.handleChange} />
              </div>
              <div className="form-group">
                <label htmlFor="searchFor">Url Part</label>
                <input type="text" className="form-control" id="searchFor" placeholder="Url Part"
                       onChange={this.handleChange} />
              </div>
              {/* <div className="form-check">
                <input type="checkbox" className="form-check-input" id="useCache" />
                <label className="form-check-label" htmlFor="useCache">Use Caching</label>
              </div> */}
              <button type="submit" className="btn btn-primary"
                      onClick={this.handleSubmit}>Submit</button>
            </form>
          </Col>
          <table className="table">
            <thead>
              <tr>
                <th scope="col">#</th>
                <th scope="col">Fetched</th>
                <th scope="col">Search Engine</th>
                <th scope="col">Query</th>
                <th scope="col">Search For</th>
                <th scope="col">Results</th>
              </tr>
            </thead>
            <tbody>
              {this.getResultRows()}
            </tbody>
          </table>
        </Row>
      </div>
    )
  }
}

export default connect(
  state => state.seo,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Dashboard);