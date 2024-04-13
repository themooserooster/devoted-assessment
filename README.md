# Devoted Assessment

## Requirements

    * Dotnet SDK 8+

## How to Run

From the root of this repo, run the following:

    ```bash
        cd DevotedAssessment.Console
        dotnet run
    ```

## Tests

### Test 1

Input:

```plaintext
GET a
SET a foo
SET b foo
COUNT foo
COUNT bar
DELETE a
COUNT foo
SET b baz
COUNT foo
GET b
GET B
END
```

Expected Output:

```plaintext
>> GET a
NULL
>> SET a foo
>> SET b foo
>> COUNT foo
2
>> COUNT bar
0
>> DELETE a
>> COUNT foo
1
>> SET b baz
>> COUNT foo
0
>> GET b
baz
>> GET B
NULL
>> END
```

### Test 2

Input:
```plaintext
SET a foo
SET a foo
COUNT foo
GET a
DELETE a
GET a
COUNT foo
END
```

Expected Output:

```plaintext
>> SET a foo
>> SET a foo
>> COUNT foo
1
>> GET a
foo
>> DELETE a
>> GET a
NULL
>> COUNT foo
0
>> END

```

### Test 3

Input:
```plaintext
BEGIN
SET a foo
GET a
BEGIN
SET a bar
GET a
SET a baz
ROLLBACK
GET a
ROLLBACK
GET a
END
```

Expected Output:

```plaintext
>> BEGIN
>> SET a foo
>> GET a
foo
>> BEGIN
>> SET a bar
>> GET a
bar
>> SET a baz
>> ROLLBACK
>> GET a
foo
>> ROLLBACK
>> GET a
NULL
>> END
```

### Test 4

Input:
```plaintext
SET a foo
SET b baz
BEGIN
GET a
SET a bar
COUNT bar
BEGIN
COUNT bar
DELETE a
GET a
COUNT bar
ROLLBACK
GET a
COUNT bar
COMMIT
GET a
GET b
END
```

Expected Output:

```plaintext
>> SET a foo
>> SET b baz
>> BEGIN
>> GET a
foo
>> SET a bar
>> COUNT bar
1
>> BEGIN
>> COUNT bar
1
>> DELETE a
>> GET a
NULL
>> COUNT bar
0
>> ROLLBACK
>> GET a
bar
>> COUNT bar
1
>> COMMIT
>> GET a
bar
>> GET b
baz
>> END
```
