import React, { Component } from 'react'
import { Row, Col } from 'react-bootstrap'
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Seo';
import Tabs from 'react-bootstrap/lib/Tabs'
import Tab from 'react-bootstrap/lib/Tab'
import classNames from 'classnames'

class Dashboard extends Component {
  constructor(props) {
    super(props)

    this.state = {
      query: '',
      searchFor: '',
      active: ''
    }

    this.handleSubmit = this.handleSubmit.bind(this)
    this.handleChange = this.handleChange.bind(this)
    this.getResultRows = this.getResultRows.bind(this)
    this.getTabs = this.getTabs.bind(this)
    this.getResultsTable = this.getResultsTable.bind(this)
    this.switchTabs = this.switchTabs.bind(this)
  }

  componentWillMount() {
    this.props.requestSearchSources()
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

  getResultsTable(searchSource) {
    return (
    <table className="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Fetched</th>
          <th scope="col">Query</th>
          <th scope="col">Search For</th>
          <th scope="col">Results</th>
        </tr>
      </thead>
      <tbody>
        {this.getResultRows(searchSource)}
      </tbody>
    </table>)
  }

  getResultRows(searchSource) {
    const { results } = this.props
    const matchResults = results[searchSource]

    if (!matchResults) {
      return null
    }

    const total = matchResults.length

    return matchResults.map((m, i) => {
      const { matches, timestamp, query, searchFor } = m
      const time = new Date(timestamp)
      return (
        <tr key={i}>
          <th scope="row">{total - i}</th>
          <td>{time.getUTCFullYear() +"/"+ (time.getUTCMonth()+1) +"/"+ time.getUTCDate() + " " + time.getUTCHours() + ":" + time.getUTCMinutes() + ":" + time.getUTCSeconds()}</td>
          <td>{query}</td>
          <td>{searchFor}</td>
          <td>{matches.join(',')}</td>
        </tr>
      )})
  }

  getTabs() {
    const { searchSources } = this.props
    return searchSources.map(ss => (
      <Tab eventKey={ss} title={ss} key={ss}>
        {this.getResultsTable(ss)}
      </Tab>))
  }

  switchTabs(key) {
    this.setState({
      active: key
    })
  }

  render() {
    const { searchSources } = this.props
    const { active } = this.state
    const defaultSearchSource = active
      ? active
      : searchSources && searchSources.length > 0
        ? searchSources[0]
        : null

    console.log(defaultSearchSource);
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
        </Row>
        <Row style={{
          marginTop: 10,
          marginBottom: 10
        }}>
          <Col md={10}>
            <Tabs activeKey={defaultSearchSource} onSelect={this.switchTabs}  id="uncontrolled-tab">
              {this.getTabs()}
            </Tabs>
          </Col>
        </Row>
      </div>
    )
  }
}

export default connect(
  state => state.seo,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Dashboard);