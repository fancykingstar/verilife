import axios from 'axios'
import EventBus from '../globalbus'

/**
 * Create an Axios Client with defaults
 */

// todo: add config file for services
const client = axios.create({
  baseURL: process.env.API
})

if (localStorage.getItem('jwtToken')) {
  axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('jwtToken')
}

/**
 * Request Wrapper with default success/error actions
 */
const httpservice = function (options) {
  const onSuccess = function (response) {
    if (response.data && response.data.token) {
      localStorage.setItem('jwtToken', response.data.token)
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('jwtToken')
    }
    console.debug('Request Successful!', response)
    return response.data
  }

  const onError = function (error) {
    if (error.response) {
      // Request was made but server responded with something
      // other than 2xx
      console.error('Status:', error.response.status)
      console.error('Data:', error.response.data)
      console.error('Headers:', error.response.headers)
      if (error.response.status === 401) {
        EventBus.$emit('SESSION_TIMEOUT')
      } else {
        EventBus.$emit('SERVER_ERROR', error.response.data.error)
      }
    } else {
      console.error('Error Message:', error.message)
    }
    return Promise.reject(error.response || error.message)
  }

  return client(options)
    .then(onSuccess)
    .catch(onError)
}

export default httpservice
