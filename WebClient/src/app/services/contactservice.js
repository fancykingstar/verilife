import httpservice from './httpservice'

var controller = '/contact/'

function contactGetByCardId (id) {
  return httpservice({
    url: controller + 'ContactGetByCardIdV2',
    method: 'GET',
    params: {
      CardID: id
    }
  })
}

function publishedOffersGet (id, company) {
  return httpservice({
    url: controller + 'PublishedOffersGet',
    method: 'GET',
    params: {
      CardID: id,
      Company: company
    }
  })
}

function contactUpdate (mb) {
  return httpservice({
    url: controller + 'ContactUpdate',
    method: 'POST',
    data: mb
  })
}
function contactRegister (re) {
  return httpservice({
    url: controller + 'ContactRegister',
    method: 'POST',
    params: {
      RegInfo: re
    }
  })
}
function contactChangePassword (id, oldPass, newPass) {
  return httpservice({
    url: controller + 'contactChangePassword',
    method: 'POST',
    params: {
      AccountID: id,
      Old: oldPass,
      New: newPass
    }
  })
}
function contactSetAttributes (data) {
  return httpservice({
    url: controller + 'ContactSetAttributes',
    method: 'POST',
    data: {
      json: data
    }
  })
}

function contactGetAttributes (data) {
  return httpservice({
    url: controller + 'ContactGetAttributes',
    method: 'GET',
    params: {
      AccountID: data
    }
  })
}

function contactRegisterByEmail (name, email, password) {
  return httpservice({
    url: controller + 'contactRegisterByEmail',
    method: 'POST',
    params: {
      Name: name,
      Email: email,
      Password: password
    }
  })
}
function contactLogin (login) {
  return httpservice({
    url: controller + 'ContactLogin',
    method: 'POST',
    params: {
      LoginInfo: login
    }
  })
}
function getContactByExternalID (id) {
  return httpservice({
    url: controller + 'GetContactByExternalID',
    method: 'GET',
    params: {
      ExternalID: id
    }
  })
}

function resetPassword (email) {
  return httpservice({
    url: controller + 'ResetPassword',
    method: 'GET',
    params: {
      Email: email
    }
  })
}

function notificationsGetByContactId (id) {
  return httpservice({
    url: controller + 'NotificationsGetByContactId',
    method: 'GET',
    params: {
      ContactID: id
    }
  })
}

function getRefundList (id) {
  return httpservice({
    url: controller + 'GetRefundList',
    method: 'GET',
    params: {
      ContactID: id
    }
  })
}

function getPublishedItems (id) {
  return httpservice({
    url: controller + 'GetPublishedItems',
    method: 'GET',
    params: {
      ContactID: id
    }
  })
}

function getItemDetails (id) {
  return httpservice({
    url: controller + 'GetItemDetails',
    method: 'GET',
    params: {
      ItemNo: id
    }
  })
}

function getItemsPerGroup (groupCode, page) {
  return httpservice({
    url: controller + 'getItemsPerGroup',
    method: 'GET',
    params: {
      GroupCode: groupCode,
      Page: page
    }
  })
}

function getDigitalReceiptJSON (storeNo, posTerminalNo, transactionNo, company) {
  return httpservice({
    url: controller + 'GetDigitalReceiptJSON',
    method: 'GET',
    params: {
      StoreNo: storeNo,
      PosTerminalNo: posTerminalNo,
      TransactionNo: transactionNo,
      Company: company
    }
  })
}

function getFeaturedWatch () {
  return httpservice({
    url: controller + 'GetFeaturedWatch',
    method: 'GET'
  })
}

function getLocations () {
  return httpservice({
    url: controller + 'GetLocations',
    method: 'GET'
  })
}

function contactCheckByEmail (email) {
  return httpservice({
    url: controller + 'ContactCheckByEmail',
    method: 'GET',
    params: {
      Email: email
    }
  })
}

function getAddressByZip (code) {
  return httpservice({
    url: controller + 'GetAddressByZip',
    method: 'GET',
    params: {
      Code: code
    }
  })
}

function getOfferHtml (code) {
  return httpservice({
    url: controller + 'GetOfferHtml',
    method: 'GET',
    params: {
      Code: code
    }
  })
}

const taskService = {
  getOfferHtml,
  getAddressByZip,
  contactCheckByEmail,
  contactGetByCardId,
  publishedOffersGet,
  contactUpdate,
  getContactByExternalID,
  contactRegister,
  contactLogin,
  resetPassword,
  contactChangePassword,
  notificationsGetByContactId,
  contactRegisterByEmail,
  getPublishedItems,
  contactSetAttributes,
  contactGetAttributes,
  getItemDetails,
  getItemsPerGroup,
  getDigitalReceiptJSON,
  getRefundList,
  getFeaturedWatch,
  getLocations
}

export default taskService
