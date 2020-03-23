
import { writeFile } from 'fs';
import { argv } from 'yargs';

// This is good for local dev environments, when it's better to
// store a projects environment variables in a .gitignore'd file
const fs = require('fs');
require('dotenv').config();

// Would be passed to script like this:
// `ts-node set-env.ts --environment=dev`
// we get it from yargs's argv object
const environment = argv.environment;
const isProd = environment === 'prod';

const targetPath = `./src/environments/environment.ts`;
const envConfigFile = `
export const environment = {
    production: false,
    apiUrl: '/api/',
    docUrl: '/Docs/',
    oldUiUrl: 'http://localhost:4000/#/',
    notifyHubUrl: '/notifyhub/',
    hubUrl: '/notifyhub/',
    uploadUrl: 'https://storage.cloud.google.com/' // proposal doc
    , // proposal doc
      Auth0Config: {
       // clientID: '5ZEGIS2KMQmnoLGgEiAgIya2wPW2rf5B',
       clientID: "${process.env.AUTH_WEBAPP_CLIENT_ID}",
       domain: "${process.env.AUTH_TENANT_DOMAIN}",
       // callbackURL: 'https://localhost:5001/',
       // apiUrl: 'https://localhost:5001/'
       callbackURL: "${process.env.AUTH_WEBAPP_CALLBACK_URL}",
       apiUrl: "${process.env.AUTH_WEBAPP_API_URL}"
    }
};
`;
writeFile(targetPath, envConfigFile, function (err) {
  if (err) {
    console.log(err);
  }

  console.log(`Output generated at ${targetPath}`);
});
