const apiUrl = 'http://localhost:5033';

export const environment = {
  apiUrl: apiUrl,
  removeContactUrl: `${apiUrl}/Contacts`,
  addContactUrl: `${apiUrl}/Contacts`,
  editContactUrl: `${apiUrl}/Contacts`,
  allContactsUrl: `${apiUrl}/Contacts`,
  contactDetailsUrl: `${apiUrl}/Contacts`,
  loginUrl: `${apiUrl}/User/jwt`,
  logoutUrl: `${apiUrl}/User/jwt`,
  registerUrl: `${apiUrl}/User`,
  getUsernameUrl: `${apiUrl}/User/username`,
  categoriesUrl: `${apiUrl}/Dictionary/categories`,
  subcategoriesUrl: `${apiUrl}/Dictionary/subcategories`,
  hidePassword: true
};