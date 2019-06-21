import { FileSourceEntityTypes } from '../enum';

 export class SignedUrlObjectName {

  static getSignedURLObjectName(pageId: number): string {
    let objectName = '';

    switch (pageId) {
      case FileSourceEntityTypes.Voucher:
      objectName = 'voucher/';
      break;
    }

    return objectName;
  }
 }
