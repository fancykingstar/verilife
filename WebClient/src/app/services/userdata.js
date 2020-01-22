var storageKey = 'CONTACT'

function getUserData () {
  var contacString = localStorage.getItem(storageKey)
  if (contacString) {
    var contact = JSON.parse(contacString)
    var contactExtInfo = JSON.parse(contact.externalInfo)
    contact.externalInfo = contactExtInfo
    return contact
  }
  return null
}

function setUserData (data) {
  localStorage.setItem(storageKey, JSON.stringify(data))
}

function removeUserData () {
  localStorage.removeItem(storageKey)
  localStorage.removeItem('jwtToken')
}

const userData = {
  getUserData,
  setUserData,
  removeUserData
}

export default userData
