
---
# Korean
<br />

# JCODD

JCODD는 JSON 데이터를 더 작고 효율적인 형식으로 변환하기 위한 JavaScript 라이브러리입니다. 이 라이브러리는 JSON 데이터를 압축하고, 다시 원래의 JSON 형식으로 복원하는 기능을 제공합니다. 최종적으로 BASE64로 인코드 했을 때 적은 길이의 텍스트를 얻는 것을 목표로 작성되었습니다.

## 주요 기능

- **JSON 데이터 압축**: JSON 데이터를 JCODD 형식으로 변환하여 더 작은 크기로 압축할 수 있습니다.
- **JCODD 데이터 복원**: JCODD 형식의 데이터를 원래의 JSON 형식으로 복원할 수 있습니다.
- **객체 직접 변환**: JavaScript 객체를 직접 JCODD 형식으로 변환하거나 JCODD 형식에서 객채로 변환할 수 있습니다.

## 설치

JCODD는 NPM을 통해 설치할 수 있습니다.

```bash
npm install jcodd
```

또는 classic .js 파일로도 사용할 수 있습니다.

## 사용법

### JSON 데이터 압축

```javascript
const json = '{"name":"Jane","age":30,"city":"Incheon"}';
const jcodd = Jcodd.toCodd(json);
console.log(jcodd); // 압축된 JCODD 형식의 데이터 출력
```

### JCODD 데이터 복원

```javascript
const jcodd = '...'; // JCODD 형식의 데이터
const json = Jcodd.toJson(jcodd);
console.log(json); // 원래의 JSON 데이터 출력
```

### 객체 직접 변환

```javascript
const obj = { name: "Jane", age: 30, city: "Incheon" };
const jcodd = Jcodd.coddify(obj);
console.log(jcodd); // 객체를 JCODD 형식으로 변환하여 출력
```

### JCODD 형식에서 객체로 변환

```javascript
const jcodd = '...'; // JCODD 형식의 데이터
const obj = Jcodd.decoddify(jcodd);
console.log(obj); // JCODD 형식의 데이터를 객체로 변환하여 출력
```

### Jcodd 객체 이용

```javascript
const origin = { name: "Jane", age: 30, city: "Incheon" };

const jc = JCODD(origin);
console.log(jc.base64); // 객체를 BASE64 JCODD 형식으로 변환하여 출력

const jc1 = JCODD(jc.json);
jc1.obj.name = "John";
console.log(jc1.jcodd); // JSON 코드를 JCODD 형식으로 변환하여 출력

const jc2 = JCODD(jc1.jcodd);
jc2.obj.age = 29;
console.log(jc2.json); // JCODD 코드를 JSON 형식으로 변환하여 출력

const jc3 = JCODD(jc2.base64);
jc3.obj.city = "Daejeon";
console.log(jc3.obj); // BASE64 JCODD 형식에서 객체로 변환하여 출력

console.log("data: " + jc3); // String 자동 형변환 시 JCODD 형식으로 출력
```


## 기여

기여를 원하시면, 이 저장소를 포크하고 풀 리퀘스트를 보내주세요. 버그 리포트와 기능 요청은 이슈 트래커를 통해 제출할 수 있습니다.

JS 이외 추가 언어로 포팅된 버전을 작성하여 풀 리퀘스트 하시면 프로젝트에 추가하도록 할 예정입니다.

## 라이선스

이 프로젝트는 MIT 라이선스 하에 배포됩니다. 자세한 내용은 `LICENSE` 파일을 참조하세요.

<br />

---
# English
<br />

# JCODD

JCODD is a JavaScript library for converting JSON data into a smaller and more efficient format. This library provides functionality to compress JSON data and restore it back to its original JSON format. It is designed to achieve shorter text length when encoded in BASE64.

## Key Features

- **JSON Data Compression**: Convert JSON data into JCODD format to compress it into a smaller size.
- **JCODD Data Restoration**: Restore data in JCODD format back to its original JSON format.
- **Direct Object Conversion**: Convert JavaScript objects directly to JCODD format or from JCODD format back to objects.

## Installation

You can install JCODD via NPM:

```bash
npm install jcodd
```

Or use it as a classic .js file.

## Usage

### JSON Data Compression

```javascript
const json = '{"name":"Jane","age":30,"city":"Incheon"}';
const jcodd = Jcodd.toCodd(json);
console.log(jcodd); // Outputs compressed JCODD format data
```

### JCODD Data Restoration

```javascript
const jcodd = '...'; // JCODD format data
const json = Jcodd.toJson(jcodd);
console.log(json); // Outputs original JSON data
```

### Direct Object Conversion

```javascript
const obj = { name: "Jane", age: 30, city: "Incheon" };
const jcodd = Jcodd.coddify(obj);
console.log(jcodd); // Outputs object converted to JCODD format
```

### Conversion from JCODD Format to Object

```javascript
const jcodd = '...'; // JCODD format data
const obj = Jcodd.decoddify(jcodd);
console.log(obj); // Outputs JCODD format data converted to object
```

### Using Jcodd Object

```javascript
const origin = { name: "Jane", age: 30, city: "Incheon" };

const jc = JCODD(origin);
console.log(jc.base64); // Outputs object converted to BASE64 JCODD format

const jc1 = JCODD(jc.json);
jc1.obj.name = "John";
console.log(jc1.jcodd); // Converts JSON code to JCODD format and outputs it

const jc2 = JCODD(jc1.jcodd);
jc2.obj.age = 29;
console.log(jc2.json); // Converts JCODD code to JSON format and outputs it

const jc3 = JCODD(jc2.base64);
jc3.obj.city = "Daejeon";
console.log(jc3.obj); // Converts BASE64 JCODD format to object and outputs it

console.log("data: " + jc3); // Outputs JCODD format when automatically typecast to string
```


## Contribution

If you wish to contribute, please fork this repository and send a pull request. Bug reports and feature requests can be submitted through the issue tracker.
   
If you port the library to additional languages other than JS and submit a pull request, we will consider adding it to the project.


## License

This project is distributed under the MIT License. See the `LICENSE` file for more details.